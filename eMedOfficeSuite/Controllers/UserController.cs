using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using DataEntities.UserEntity;
using DataEntities.Users;
using DataLog;
using eMedOfficeSuite.ApiClient;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace eMedOfficeSuite.Controllers
{
    public class UserController : Controller
    {
        public IAuthenticationManager Authentication => HttpContext.GetOwinContext().Authentication;
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(UserLogin model)
        {
            if (ModelState.IsValid)
            try
            {
                var _apiClient = new ApiClient<UserIdentity>();

                var login = _apiClient.Auth(model.username, model.password);

                    if (login != null)
                    {

                        UserIdentity user = _apiClient.Post(_apiClient.UserUrl, login["access_token"].ToString());

                        var claims = new List<Claim>();

                        claims.Add(new Claim(ClaimTypes.Name, user.username));
                        claims.Add(new Claim(ClaimTypes.Role, user.roleName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.username));
                        claims.Add(new Claim(ClaimTypes.Thumbprint, user.userId.ToString()));
                        claims.Add(new Claim(ClaimTypes.Authentication, login["access_token"].ToString()));

                        var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                        Authentication.SignIn(identity);
                    }

            }
            catch (Exception ex)
            {
                Log.AddEntry(ex);
            }
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            var context = System.Web.HttpContext.Current.GetOwinContext();
            context.Authentication.SignOut();

            return null;
        }

        [Authorize(Roles = (DataEntities.Users.UserRolesNames.Therapist))]
        public ActionResult Test()
        {
            return View();
        }
    }
}