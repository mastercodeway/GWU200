using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PixelCritic.WebApp.Dtos;
using PixelCritic.WebApp.Repositories;
using PixelCritic.WebApp.Services;
using System.Security.Claims;
using System.Security.Principal;
using PixelCritic.WebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace PixelCritic.WebApp.Controllers
{
    public class UserController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly ILoginService _loginService;
        public UserController(ILoginService loginService, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _loginService = loginService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserLoginDto userLoginDto)
        {
            if (await _loginService.Authenticate(userLoginDto))
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userLoginDto.Username),
                        new Claim(ClaimTypes.NameIdentifier, userLoginDto.Id.ToString()),
                    };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = "Wrong password or username";
            return RedirectToAction("Index", "User");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public IActionResult UserProfile()
        {

            return View();
        }
        public async Task<IActionResult> DeleteProfile(string password, string confirmPassword)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Plese provide all the information";
                return RedirectToAction("Userprofile", "User");
            }
            var isValid = password == confirmPassword;
            if (!isValid)
            {
                ViewBag.Error = "Wrong password provided";
                return RedirectToAction("Userprofile", "User");
            }
            var userLoginDto = new UserLoginDto
            {
                Username = User.FindFirst(ClaimTypes.Name)?.Value,
                Password = password
            };
            if (await _loginService.Authenticate(userLoginDto))
            {
                await _unitOfWork.UserRepo.DeleteAsync(user => user.Username == userLoginDto.Username);
                await _unitOfWork.SaveAsync();

            }


            return await Logout();


        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string newPassword, string oldPassword)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Error occurred data missing try again";
                return RedirectToAction("UserProfile", "User");
            }
            var loginDto = new UserLoginDto
            {
                Password = oldPassword,

                Username = User.FindFirst(ClaimTypes.Name).Value
            };
            var isValid = await _loginService.Authenticate(loginDto);
            if (isValid)
            {
                var hashedPassword = _loginService.HashPassword(newPassword);

                await _unitOfWork.UserRepo.UpdateAsync(
                    filter: user => user.Username == loginDto.Username,
                    updateExpr: update => update.SetProperty(u => u.Password, hashedPassword));
               await  _unitOfWork.SaveAsync();

            }
            else
            {
                ViewBag.ErrorMessage = "Wrong password";
            }
            return RedirectToAction("UserProfile", "User");

        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var success = await _loginService.Register(userRegisterDto);
            if (success)
            {
                return RedirectToAction("Index", "User");

            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "User");
        }

    }
}
