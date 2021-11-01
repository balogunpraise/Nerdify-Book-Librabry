﻿using LearningAuth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;

namespace LearningAuth.Controllers
{
    public class AuthController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signinManager;
        private readonly IEmailSender _emailSender;
        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser>signInManager
         , IEmailSender emailSender)
        {
            _userManager = userManager;
            _signinManager = signInManager;
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Register(string redirecturl = null)
        {
            ViewData["RedirectUrl"] = redirecturl;
            RegisterViewModel model = new();
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string redirecturl = null)
        {
            ViewData["RedirectUrl"] = redirecturl;
            redirecturl ??= Url.Content("~/");

            if (model.UserName is null)
            {
                model.UserName = model.Email;
            }
            if (ModelState.IsValid)
            {
                var user = new AppUser { FirstName = model.FirstName, LastName = model.LastName, Email = model.Email, UserName = model.UserName };
               
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signinManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(redirecturl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Opps! Something went wrong");
                }
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Login(string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            LoginViewModel model = new();
            return View(model);
        }




        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var result = await _signinManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return LocalRedirect(returnurl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Credentials");
                }
            }
            return View(model);
        }



        public async Task<ActionResult> Logout()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }





       
        public IActionResult ForgotPassword()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user is null)
                {
                    ModelState.AddModelError(string.Empty, "The email is invalid");
                    return RedirectToAction("ForgotPasswordConfimation", "Auth");
                }
                else
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callBack = Url.Action("ResetPassword", "Auth", new { Id = user.Id, code = code },
                    protocol: HttpContext.Request.Scheme);
                    await _emailSender.SendEmailAsync(model.Email, "Password Reset",
                        "Please reset your password by clicking this: <a href=\"" + callBack + "\">Link</a>");
                    return RedirectToAction("ForgotPasswordConfirmation");
                }
            }
            return View();
            
        }



        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

    }
}
