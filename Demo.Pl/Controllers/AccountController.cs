using Demo.DAL.Models;
using Demo.Pl.Helper;
using Demo.Pl.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Demo.Pl.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSettings _emailSettings;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSettings emailSettings)
        {
			_userManager = userManager;
			_signInManager = signInManager;
            _emailSettings = emailSettings;
        }

    
      
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)//Server Side Validation
            {
                //ManualMapping
                var user = new ApplicationUser()
                {
                    FName=model.FName,
                    LName=model.LName,
                    UserName=model.Email,
                    Email=model.Email,
                    IsAgree=model.IsAgree

                };

                var result = await _userManager.CreateAsync(user,model.Password );

                if(result.Succeeded)
                    return RedirectToAction(nameof(Login));

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description); 
                
            }
            return View(model);
        }
   

        
		public async Task<IActionResult> Login(LoginViewModel model)
		{
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if(result.Succeeded)
                      return  RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError(string.Empty, "Password is not correct!");
                }
                ModelState.AddModelError(string.Empty, "Email is not existed!");
            }

            return View(model);
		}

        public new async Task<IActionResult> SignOut()
        {
            await   _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));


        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user =await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var token =await _userManager.GeneratePasswordResetTokenAsync(user);//valid to this user only One-Time
                    var passwordResetLink = Url.Action("ResetPassword", "Account", new { email = model.Email, Token=token},/*"https", "localhost:44376"*/Request.Scheme);
                    //https://localhost:44376/Account/ResetPassword?email=otesma@gmail.com&Token=kladfjasdjf
                    var email = new Email()
                    {
                        Subject = "Reset Passowrd",
                        To = user.Email,
                        Body = passwordResetLink
                    };
                    _emailSettings.SendEmail(email);
                    //EmailSetting.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
                ModelState.AddModelError(string.Empty, "The Email is not existed!");
            }
            return View(model);
        }

        public IActionResult CheckYourInbox()
        {
          return  View();
        }

        #region ResetPassword
        public  IActionResult ResetPassword(string email,string Token)
        {
            TempData["Token"] = Token;
            TempData["email"] = email;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {

               string email = TempData["email"]as string;
               string token =  TempData["Token"] as string;
                var user = await _userManager.FindByEmailAsync(email);
                var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));
				foreach (var error in result.Errors)
                 ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model); 
        }
		#endregion
    }
	}
