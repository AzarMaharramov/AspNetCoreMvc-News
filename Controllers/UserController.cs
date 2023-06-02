using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFirstProject.Models;
using MyFirstProject.Repositories;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MyFirstProject.Controllers
{
    public class UserController : Controller
    {
        UserRepository userRepository = new UserRepository();
        public IActionResult Index()
        {
            #region Session
            /*
                var session = HttpContext.Session.GetInt32("UserId");
                string? name = HttpContext.Session.GetString("Name");
                if (session == null)
                {
                    return RedirectToAction("Login");
                } 
            */
            #endregion 

            return View(); //(object)name
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            #region Session
            /*
                var userId = HttpContext.Session.GetInt32("UserId");
                if (userId != null)
                    return RedirectToAction("Index");
            */
            #endregion

            ViewBag.Username = Request.Cookies["username"];

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string username = model.Username;
                string password = string.Join(string.Empty, MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(model.Password.Trim())).Select(p => p.ToString("x2")));
                User? user = userRepository.GetUser(username, password).FirstOrDefault();
                if (user != null)
                {
                    if (model.RememberMe)
                    {
                        Response.Cookies.Append(
                            "username",
                            model.Username,
                            new CookieOptions { Expires = DateTimeOffset.Now.AddDays(7) });
                    }
                    else Response.Cookies.Delete("username");

                    string adminUser = user.IsAdmin ? "Admin" : "User";

                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, adminUser)
                    };

                    var useridentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);

                    HttpContext.SignInAsync(principal);

                    #region Session
                    //HttpContext.Session.SetInt32("UserId", user.Id);
                    //HttpContext.Session.SetString("Name", user.Name);
                    #endregion

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.message = "Username or password is incorrect";
                }
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            #region Session
            //HttpContext.Session.Clear();
            #endregion
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public IActionResult CreateUser()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            user.Password = string.Join(string.Empty, MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(user.Password.Trim())).Select(p => p.ToString("x2")));
            userRepository.CreateUser(user);
            ViewBag.SuccessAdd = "Registration completed successfully!";

            return RedirectToAction("Login");
        }

        public IActionResult ResetPassword()
        {
            return View();
        }
        public IActionResult Tables()
        {
            return View();
        }
        public IActionResult Charts()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

