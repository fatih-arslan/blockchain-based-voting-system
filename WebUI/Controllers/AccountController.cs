using DataAccess.Data;
using DataAccess.Static;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _Context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _Context = context;
        }
      
        public IActionResult Login()
        {
            return View(new LoginVM());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if(!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var user = await _userManager.FindByNameAsync(loginVM.IdentificationNumber);
            if(user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);

                if(passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                TempData["Error"] = "Wrong credentials please try again";
                return View(loginVM);
            }
            TempData["Error"] = "Wrong credentials please try again";
            return View(loginVM);
        }

        public async Task<IActionResult> Register()
        {
            return View(new RegisterVM());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if(!ModelState.IsValid)
            {
                return View(registerVM);
            }
            var user = await _userManager.FindByNameAsync(registerVM.IdentificationNumber);
            if(user != null)
            {
                TempData["Error"] = "This Identification number is already in use";
                return View(registerVM);
            }

            var newUser = new ApplicationUser
            {
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                IdentificationNumber = registerVM.IdentificationNumber,
                Email = registerVM.EmailAdress,
                UserName = registerVM.IdentificationNumber
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);
            if(newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.Voter);
            }
            return View("RegisterCompleted");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
