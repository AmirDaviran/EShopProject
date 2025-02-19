using EShop.Application.Interfaces;
using EShop.Domain.ViewModels.Account;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using EShop.Application.Utilities.Convertors;

namespace EShop.Web.Controllers
{
    public class AccountController(IUserService userService) : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        #region Register

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUser(register);
                switch (result)
                {
                    case RegisterResult.EmailExists:
                        TempData[ErrorMessage] = "ایمیل وارد شده قبلا در سیستم ثبت شده است";
                        break;
                    case RegisterResult.Success:
                        TempData[SuccessMessage] = "ثبت نام شما با موفقیت انجام شد. لطفا روی لینک ارسال شده کلیک کنید.";
                        return RedirectToAction("Login");
                }
            }
            return View(register);
        }

        #endregion

        #region Login
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if(ModelState.IsValid)
            {
                var result = await _userService.LoginUser(login);
                switch(result)
                {
                    case LoginResult.NotFound:
                        TempData[WarningMessage] = "کاربری یافت نشد";
                        break;
                    case LoginResult.NotActive:
                        TempData[ErrorMessage] = "حساب کاربری شما فعال نمی باشد";
                        break;
                    case LoginResult.IsBlocked:
                        TempData[WarningMessage] = "حساب شما توسط واحد پشتیبانی مسدود شده است";
                        TempData[InfoMessage] = "جهت اطلاع بیشتر لطفا به قسمت تماس باما مراجعه کنید";
                        break;
                    case LoginResult.Success:
                        var user = await _userService.GetUserByEmail(login.Email);
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,user.Email),
                            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                            new Claim("FirstName", user.FirstName?? string.Empty), // Add first name claim  
                            new Claim("LastName", user.LastName ?? string.Empty),  // Add last name claim  
                            new Claim("Phone-Number", user.PhoneNumber?? string.Empty)
                           
                        };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principle = new ClaimsPrincipal(identity);
                        var properties = new AuthenticationProperties
                        {
                            IsPersistent = login.RememberMe
                        };
                        await HttpContext.SignInAsync(principle, properties);
                        TempData[SuccessMessage] = "ورود شما با موفقیت انجام شد";
                        return Redirect("/");
                }
            }

            return View(login);
        }

        #endregion

        #region LogOut

        [HttpGet("logOut")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData[InfoMessage] = "خروج شما با موفقیت انجام شد";
            return Redirect("/");
        }
        #endregion

        #region Activation Account

        [Route("Account/ActiveAccount/{code}")]
        public async Task<IActionResult> ActiveAccount(string code)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ActivateAccount(code);
                switch (result)
                {
                    case ActivateAccountResult.Error:
                        TempData[ErrorMessage] = "عملیات فعال کردن حساب کاربری با شکست مواجه شد";
                        break;
                    case ActivateAccountResult.NotFound:
                        TempData[WarningMessage] = "کاربری با مشخصات وارد شده یافت نشد";
                        break;
                    case ActivateAccountResult.Success:
                        TempData[SuccessMessage] = "حساب کاربری شما با موفقیت فعال شد. لطفا جهت ادامه فراید وارد حساب کاربری خود شود";
                        //TempData[InfoMessage] = "لطفا جهت ادامه فراید وارد حساب کاربری خود شود";
                        return RedirectToAction("Login");
                }
            }
            return Redirect("Register");

        }

        #endregion

        #region Forgot Password
        [HttpGet]
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("ForgotPassword"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (ModelState.IsValid)
            {
                ForgotPasswordViewModel forgotPassword = new ForgotPasswordViewModel()
                {
                    Email = FixedText.FixEmail(email),
                    Code=""
                };
                var result = await _userService.ForgotPasswrdUser(forgotPassword);

                switch (result)
                {
                    case ForgotPasswordResult.NotFound:
                        TempData[WarningMessage] = "کاربری با مشخصات وارد شده یافت نشد";
                        break;
                    case ForgotPasswordResult.Success:
                        TempData[SuccessMessage] = "لطفا روی لینک بازیابی گذرواژه ارسال شده در ایمیلتان کلیک کنید. ";
                        return RedirectToAction("Login");

                }
            }
            return View();
            }

        [HttpGet]
        [Route("Account/ResetPassword/{activeCode}")]
        public ActionResult ResetPassword(string activeCode)
        {
            return View(new ResetPasswordViewModel(){
                ActiveCode = activeCode
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ResetPassword(resetPassword);
                switch (result)
                {
                    case ResetPasswordResult.NotFound:
                        TempData[WarningMessage] = "کاربری با مشخصات وارد شده یافت نشد";
                        break;
                    case ResetPasswordResult.Success:
                        TempData[SuccessMessage] = "رمز عبور با موفقیت تغییر یافت.";
                        return RedirectToAction("Login");
                }

            }
            return View();
        }
        #endregion


    }
}
