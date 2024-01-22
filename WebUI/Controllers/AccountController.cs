using DataAccess.Data;
using DataAccess.Static;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;            
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Users()
        {
            IEnumerable<ApplicationUser> users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        [Authorize(Roles = UserRoles.Voter)]
        public async Task<IActionResult> Profile()
        {
            ApplicationUser? user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [Authorize(Roles = UserRoles.Voter)]
        public async Task<IActionResult> Edit()
        {
            ApplicationUser? user = await _userManager.GetUserAsync(User);

            if(user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser newUser)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(newUser.IdentificationNumber);
                if(user != null)
                {
                    user.FirstName = newUser.FirstName;
                    user.Lastname = newUser.Lastname;
                    user.Email = newUser.Email;
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction("Profile");
                }
                return View("NotFound");
               
            }
            return View(newUser);
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
                        user = await _userManager.Users
                        .Include(u => u.Votes)
                        .FirstOrDefaultAsync(u => u.Id == user.Id);
                        var claims = new List<Claim>
                        {
                            new Claim("FirstName", user.FirstName),
                            new Claim("LastName", user.Lastname),
                        };

                        await _userManager.AddClaimsAsync(user, claims);
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
                FirstName = registerVM.Name,
                Lastname = registerVM.Surname,
                IdentificationNumber = registerVM.IdentificationNumber,
                Email = registerVM.EmailAdress,
                UserName = registerVM.IdentificationNumber,
                RegistrationDate = DateTime.Now
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
