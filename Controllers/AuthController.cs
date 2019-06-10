/* Filename: CartController.cs
 * Description: This class is responsible for handing the user authorization and authentication.
 * 
 * Revision History:
 *     Ryan Pease, 2016-11-22: Created 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoGameStore.Models;
using System.Security.Claims;
using System.Web.Helpers;
using System.Data.Entity.Validation;

namespace VideoGameStore.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        // GET: Auth
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                string password = model.Password;
                VideoGameStoreDBContext db = new VideoGameStoreDBContext();
                var users = db.Users.Where(u => u.email == model.Email).ToList();
                if (users.Count == 1)
                {
                    User user = users.FirstOrDefault();
                    string hashedPassword = user.user_password;
                    if (CheckPassword(password, hashedPassword))
                    {
                        var role = "";
                        if (user.is_admin)
                        {
                            role = "Admin";
                        }
                        else if (user.is_employee)
                        {
                            role = "Employee";
                        }
                        else if (user.is_member)
                        {
                            role = "Member";    //necessary?
                        }
                        else
                        {
                            role = "Customer";
                        }
                        var identity = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.Name, user.username),
                            new Claim(ClaimTypes.Email, user.email),
                            new Claim(ClaimTypes.Role, role)
                            },
                            "ApplicationCookie");
                        var context = Request.GetOwinContext();
                        var authManager = context.Authentication;

                        authManager.SignIn(identity);

                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "Incorrect password.");
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError("", "Email address not found.");
                    return View(model);
                }
            }

            else
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }
        }

        public ActionResult Logout()
        {
            var context = Request.GetOwinContext();
            var authManager = context.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Auth");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                string password = user.user_password;
                string hashedPassword = Crypto.HashPassword(password);

                user.user_password = hashedPassword;

                VideoGameStoreDBContext db = new VideoGameStoreDBContext();
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "One or more fields are invalid");
                return View();
            }
        }

        public bool CheckPassword(string plainTextPassword, string hashedPassword)
        {
            VideoGameStoreDBContext db = new VideoGameStoreDBContext();
            return Crypto.VerifyHashedPassword(hashedPassword, plainTextPassword);
        }
    }
}