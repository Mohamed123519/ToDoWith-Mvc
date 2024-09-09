using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoAPP.Context;
using TodoAPP.Models;
using TodoAPP.ViewModels;

namespace TodoAPP.Controllers
{
    public class AccountController : Controller
    {
        TodoContext context;
        public AccountController()
        {
            context = new TodoContext();
        }

        public IActionResult Resgister()
        {
            return View();
        }

        public IActionResult SubmitRegister(CreateUser user)
        {
            if (!ModelState.IsValid)
            {
                return View("Resgister", user);
            }
            var newUser = new User()
            {
                Email = user.Email,
                Name = user.Name,
                Password = user.Password,
                Role = user.Role,
            };
            context.Users.Add(newUser);
            context.SaveChanges();
            return RedirectToAction("Index", "Todo");
        }




        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> SubmitLogin(LoginCred login)
        {

            var user = context.Users.FirstOrDefault(u => (u.Email == login.Email && u.Password == login.Password));
            if (user == null)
            {
                ModelState.AddModelError("", "Wrong Email Or Password");
                return View("Login", login);
            }

            var claimsidentity = new ClaimsIdentity("Cookie");
            claimsidentity.AddClaim(new Claim("Id", user.Id.ToString()));
            claimsidentity.AddClaim(new Claim("Role", user.Role.ToString()));
            var princable = new ClaimsPrincipal(claimsidentity);
            await HttpContext.SignInAsync("Cookie", princable, new AuthenticationProperties()
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddHours(1),
            });
            return RedirectToAction("Index", "Todo");
        }

        public ActionResult AccessDenied()
        {

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Todo");
        }
    }
}
