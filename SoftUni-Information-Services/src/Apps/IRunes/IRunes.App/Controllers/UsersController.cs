using IRunes.Data;
using IRunes.Models;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
<<<<<<< HEAD
using IRunes.Services;
using SIS.MvcFramework.Attributes.Action;
using SIS.WebServer.Result;
=======
>>>>>>> 5fc0bbc7dc1ca27022020ca9c0b703917b7fbd37

namespace IRunes.App.Controllers
{
    public class UsersController : Controller
    {
<<<<<<< HEAD
        private readonly UserService userService;

        public UsersController()
        {
            this.userService = new UserService();
        }
        [NonAction]
=======
>>>>>>> 5fc0bbc7dc1ca27022020ca9c0b703917b7fbd37
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                return Encoding.UTF8.GetString(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }

<<<<<<< HEAD
        public ActionResult Login()
=======
        public IHttpResponse Login(IHttpRequest httpRequest)
>>>>>>> 5fc0bbc7dc1ca27022020ca9c0b703917b7fbd37
        {
            return this.View();
        }

        [HttpPost(ActionName = "Login")]
<<<<<<< HEAD
        public ActionResult LoginConfirm()
        {
            string username = ((ISet<string>)this.Request.FormData["username"]).FirstOrDefault();
            string password = ((ISet<string>)this.Request.FormData["password"]).FirstOrDefault();

            User userFromDb = userService.GetUserByUsernameAndPassword(username, this.HashPassword(password));
                                                       
            if (userFromDb == null)
            {
                return this.Redirect("/Users/Login");
            }

            this.SignIn(userFromDb.Id, userFromDb.Username, userFromDb.Email);
            return this.Redirect("/");
        }

        public ActionResult Register()
=======
        public IHttpResponse LoginConfirm(IHttpRequest httpRequest)
        {
            using (var context = new RunesDbContext())
            {
                string username = ((ISet<string>) httpRequest.FormData["username"]).FirstOrDefault();
                string password = ((ISet<string>) httpRequest.FormData["password"]).FirstOrDefault();

                User userFromDb = context.Users.FirstOrDefault(user => (user.Username == username
                                                                        || user.Email == username)
                                                                       && user.Password == this.HashPassword(password));
                if (userFromDb == null)
                {
                    return this.Redirect("/Users/Login");
                }

                this.SignIn(httpRequest, userFromDb.Id, userFromDb.Username, userFromDb.Email);
            }

            return this.Redirect("/");
        }

        public IHttpResponse Register(IHttpRequest httpRequest)
>>>>>>> 5fc0bbc7dc1ca27022020ca9c0b703917b7fbd37
        {
            return this.View();
        }

        [HttpPost(ActionName = "Register")]
<<<<<<< HEAD
        public ActionResult RegisterConfirm()
        {
            string username = ((ISet<string>)Request.FormData["username"]).FirstOrDefault();
            string password = ((ISet<string>)Request.FormData["password"]).FirstOrDefault();
            string confirmPassword = ((ISet<string>)Request.FormData["confirmPassword"]).FirstOrDefault();
            string email = ((ISet<string>)Request.FormData["email"]).FirstOrDefault();

            if (password != confirmPassword)
            {
                return this.Redirect("/Users/Register");
            }

            User user = new User
            {
                Username = username,
                Password = this.HashPassword(password),
                Email = email
            };
            this.userService.CreateUser(user);
            return this.Redirect("/Users/Login");
        }

        public ActionResult Logout()
        {
            this.SignOut();
=======
        public IHttpResponse RegisterConfirm(IHttpRequest httpRequest)
        {
            using (var context = new RunesDbContext())
            {
                string username = ((ISet<string>)httpRequest.FormData["username"]).FirstOrDefault();
                string password = ((ISet<string>)httpRequest.FormData["password"]).FirstOrDefault();
                string confirmPassword = ((ISet<string>)httpRequest.FormData["confirmPassword"]).FirstOrDefault();
                string email = ((ISet<string>)httpRequest.FormData["email"]).FirstOrDefault();

                if (password != confirmPassword)
                {
                    return this.Redirect("/Users/Register");
                }

                User user = new User
                {
                    Username = username,
                    Password = this.HashPassword(password),
                    Email = email
                };

                context.Users.Add(user);
                context.SaveChanges();
            }

            return this.Redirect("/Users/Login");
        }

        public IHttpResponse Logout(IHttpRequest httpRequest)
        {
            this.SignOut(httpRequest);
>>>>>>> 5fc0bbc7dc1ca27022020ca9c0b703917b7fbd37

            return this.Redirect("/");
        }
    }
}
