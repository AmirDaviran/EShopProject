using EShop.Domain.Enums.UserEnums;
using EShop.Domain.Entities.Account;
using EShop.Domain.ViewModels.Account;
using EShop.Domain.ViewModels.UserViewModel;


namespace EShop.Application.Interfaces
{
   public interface IUserService
    {
        #region Account

        Task<RegisterResult> RegisterUser(RegisterViewModel register);
        Task<LoginResult> LoginUser(LoginViewModel login);
        Task<ActivateAccountResult> ActivateAccount(string activate);
        Task<User> GetUserById(int userId);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByActivationCode(string code);



        Task<ForgotPasswordResult> ForgotPasswrdUser(ForgotPasswordViewModel forgotPassword);
        Task<bool> VerifyResetCode(string code);
        Task<ResetPasswordResult> ResetPassword(ResetPasswordViewModel resetPassword);
        #endregion

        #region Admin Panel

        Task<CreateUserResult> CreateUserAsync (CreateUserViewModel model);
        Task<EditUserViewModel> GetUserForEditByIdAsync(int id);
        Task<EditUserResult> UpdateUserAsync(EditUserViewModel model);
        
        Task<bool> SoftDeleteByAdminAsync(int id);
        Task<UserDetailsViewModel> GetInformationAsync(int id);

        Task<FilterUserViewModel> FilterAsync(FilterUserViewModel model);


        #endregion
    }
}
