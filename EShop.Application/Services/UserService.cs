
using EShop.Application.Interfaces;
using EShop.Application.Models;
using EShop.Application.Security;
using EShop.Application.Utilities.Convertors;
using EShop.Application.Utilities.Generators;
using EShop.Domain.Entities.Account;
using EShop.Domain.Enums.UserEnums;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.Account;
using EShop.Domain.ViewModels.UserViewModel;
using Microsoft.EntityFrameworkCore;


namespace EShop.Application.Services
{
    public class UserService : IUserService
    {

        #region Constructor

        private readonly IViewRenderService _viewRender;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public UserService(IUserRepository userRepository, IViewRenderService viewRender,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _viewRender = viewRender;
            _emailService = emailService;
        }

        #endregion

        #region Account

        #region Register User
        public async Task<RegisterResult> RegisterUser(RegisterViewModel register)
        {
            if (!await _userRepository.IsExistsUserByEmail(register.Email))
            {
                // Hash the password before saving it  
                var hashedPassword = PasswordHelper.HashPassword(register.Password);
                var user = new User
                {
                    //FirstName = register.FirstName,
                    //LastName = register.LastName,
                    Email = register.Email,
                    Password = hashedPassword,
                    PhoneNumber = register.PhoneNumber,
                    IsActive = false,
                    //ActiveCode = CodeGeneratore.GenerateRandomCode(),
                    //ActiveCode = Guid.NewGuid().ToString(),
                    ActiveCode = NameGenerator.GenerateUniqCode(),
                    Avatar = "default.png",
                    IsDeleted = false,
                    CreatedDate = DateTime.Now
                };
                await _userRepository.CreateUser(user);
                await _userRepository.SaveChangesAsync();

                // To Do: Send Verification Email
                string body = await _viewRender.RenderToStringAsync("_ActivationEmail", user);
                var email = new Email
                {
                    To = user.Email,
                    Subject = "فعال سازی حساب کاربری",
                    Body = body
                };
                _emailService.SendVerificationEmail(email);

                return RegisterResult.Success;
            }

            return RegisterResult.EmailExists;
        }
        #endregion


        #region Login User
        public async Task<LoginResult> LoginUser(LoginViewModel login)
        {
            var user = await GetUserByEmail(login.Email);
            if (user == null) return LoginResult.NotFound;
            if (!user.IsActive) return LoginResult.NotActive;
            // Use the new PasswordHelper methods  
            if (!PasswordHelper.VerifyPassword(login.Password, user.Password))
                return LoginResult.InvalidCredentials;

            return LoginResult.Success;

        }
        #endregion


        #region Activate Account
        public async Task<ActivateAccountResult> ActivateAccount(string activate)
        {
            var user = await _userRepository.GetUserByActiveCode(activate);
            if (user == null)
                return ActivateAccountResult.NotFound;

            if (user.ActiveCode == activate)
            {
                user.ActiveCode = NameGenerator.GenerateUniqCode();
                user.IsActive = true;
                _userRepository.UpdateUser(user);
                await _userRepository.SaveChangesAsync();

                return ActivateAccountResult.Success;
            }

            return ActivateAccountResult.Error;
        }
        #endregion

        #region Get User By Email
        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }
        #endregion

        #region Get User By Id
        public async Task<User> GetUserById(int userId)
        {
            return await _userRepository.GetUserById(userId);
        }
        #endregion


        #region Get User By ActivationCode
        public async Task<User> GetUserByActivationCode(string code)
        {
            return await _userRepository.GetUserByActiveCode(code);
        }
        #endregion


        #region Forgot Password

        public async Task<ForgotPasswordResult> ForgotPasswrdUser(ForgotPasswordViewModel forgotPassword)
        {
            var user = await GetUserByEmail(forgotPassword.Email);
            if (user == null) return ForgotPasswordResult.NotFound;
            string body = await _viewRender.RenderToStringAsync("_ForgotPasswordEmail", user);

            var email = new Email
            {
                To = user.Email,
                Subject = "بازیابی کلمه عبور",
                Body = body
            };
            _emailService.SendVerificationEmail(email);

            return ForgotPasswordResult.Success;
        }

        public async Task<ResetPasswordResult> ResetPassword(ResetPasswordViewModel resetPassword)
        {
            var user = await _userRepository.GetUserByActiveCode(resetPassword.ActiveCode);
            if (user == null) return ResetPasswordResult.NotFound;
            //if(user.ActiveCode == resetPassword.Code)
            //{
            //    user.ActiveCode = resetPassword.Code;
            //}
            string hashNewPassword = PasswordHelper.HashPassword(resetPassword.Password);
            user.Password = hashNewPassword;
            _userRepository.UpdateUser(user);
          await  _userRepository.SaveChangesAsync();
            return ResetPasswordResult.Success;

        }

        public async Task<bool> VerifyResetCode(string code)
        {
            var user = await GetUserByActivationCode(code);
            if (user == null) return false;
            if (user.ActiveCode == code) return true;
            return false;
        }


        #endregion


        #endregion

        #region Admin

        public async Task<CreateUserResult> CreateUserAsync(CreateUserViewModel model)
        {
            User user = new User()
            {
                CreatedDate = DateTime.Now,
                Email = model.Email.Trim().ToLower(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                IsActive = model.IsActive,
                PhoneNumber = model.PhoneNumber,
                ActiveCode = NameGenerator.GenerateUniqCode(),
                Password = PasswordHelper.HashPassword(model.Password.Trim()),
                Avatar = "default.png",
                IsAdmin = model.IsAdmin

            };
            await _userRepository.CreateUser(user);
            await _userRepository.SaveChangesAsync();

            return CreateUserResult.Success;
        }


        public async Task<EditUserViewModel> GetUserForEditByIdAsync(int id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null)
                return null;

            return new EditUserViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                IsActive = user.IsActive,
                Mobile = user.PhoneNumber
            };
        }

        public async Task<bool> SoftDeleteByAdminAsync(int id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null)
            {
                return false;
            }

            user.IsDeleted = true;
            user.IsActive = false;
            _userRepository.UpdateUser(user);
            await _userRepository.SaveChangesAsync();

            return true;
        }

        public async Task<EditUserResult> UpdateUserAsync(EditUserViewModel model)
        {
            var user = await _userRepository.GetUserById(model.Id);
            if (user == null)
                return EditUserResult.UserNotFound;

            if (await _userRepository.DuplicatedEmailAsync(user.Id, model.Email.ToLower().Trim()))
                return EditUserResult.EmailDuplicated;

            if (await _userRepository.DuplicatedMobileAsync(user.Id, model.Mobile))
                return EditUserResult.MobileDuplicated;

            user.Email = model.Email;
            user.PhoneNumber = model.Mobile;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.IsActive = model.IsActive;

            _userRepository.UpdateUser(user);
            await _userRepository.SaveChangesAsync();

            return EditUserResult.Success;
        }

        public async Task<UserDetailsViewModel> GetInformationAsync(int id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null)
                return null;

            return new UserDetailsViewModel()
            {
                CreateDate = user.CreatedDate,
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                IsActive = user.IsActive,
                LastName = user.LastName,
                Mobile = user.PhoneNumber
            };
        }

        public async Task<FilterUserViewModel> FilterAsync(FilterUserViewModel model)
        {
            var query = _userRepository.GetUsersAsQueryable().Where(user => !user.IsDeleted);

            #region Filter

            if (!string.IsNullOrEmpty(model.FirstName))
            {
                query = query.Where(user => EF.Functions.Like(user.FirstName, $"%{model.FirstName}%"));
            }

            if (!string.IsNullOrEmpty(model.LastName))
            {
                query = query.Where(user => EF.Functions.Like(user.LastName, $"%{model.LastName}%"));
            }

            if (!string.IsNullOrEmpty(model.Email))
            {
                query = query.Where(user => EF.Functions.Like(user.Email, $"%{model.Email}%"));
            }

            if (!string.IsNullOrEmpty(model.Mobile))
            {
                query = query.Where(user => EF.Functions.Like(user.PhoneNumber, $"%{model.Mobile}%"));
            }

            if (model.IsActive.HasValue)
            {
                query = query.Where(user => user.IsActive == model.IsActive.Value);
            }
            if (!string.IsNullOrEmpty(model.SearchTerm))
            {
                query = query.Where(user => EF.Functions.Like(user.FirstName, $"%{model.SearchTerm}%") ||
                                            EF.Functions.Like(user.LastName, $"%{model.SearchTerm}%") ||
                                            EF.Functions.Like(user.Email, $"%{model.SearchTerm}%") ||
                                            EF.Functions.Like(user.PhoneNumber, $"%{model.SearchTerm}%"));
            }

            #endregion

            #region Paging

            await model.Paging(query.Select(user => new UserDetailsViewModel()
            {
                CreateDate = user.CreatedDate,
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                IsActive = user.IsActive,
                LastName = user.LastName,
                Mobile = user.PhoneNumber
            }));

            #endregion

            return model;
        }


        #endregion

    }

}

