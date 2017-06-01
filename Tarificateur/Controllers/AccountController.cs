using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;


namespace Tarificateur.Controllers
{
    [System.Web.Mvc.Authorize]
    public class AccountController : Controller
    {
        [System.Web.Mvc.AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        private const string XsrfKey = "CodePaste_$31!.2*#";

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        public ActionResult Login(ViewModels.LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingUser = Business.Lists.Db.Instance.Users.FirstOrDefault(u => u.Email == model.Email);

            if (existingUser == null)
            {
                return View(model);
            }
            else
            {
                var loginSuccess = string.Equals(Business.Common.Tools.EncryptPassword(model.Password, existingUser.Salt), existingUser.PasswordHash);

                if (loginSuccess)
                {
                    var token = Business.Common.Tools.CreateToken(existingUser);

                    var claims = new List<Claim>();

                    // create required claims
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, existingUser.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, existingUser.Email));

                    claims.Add(new Claim("token", token.Token));

                    var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                    var properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = model.RememberMe,
                        ExpiresUtc = DateTime.UtcNow.AddDays(7),

                    };
                    properties.Dictionary[XsrfKey] = existingUser.Id.ToString();

                    //HttpContext.GetOwinContext().Authentication.Challenge(properties);
                    HttpContext.GetOwinContext().Authentication.SignIn(properties, identity);


                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    return View(model);
                }
            }
        }

        //
        // POST: /Account/LogOff
        [System.Web.Mvc.HttpPost]
        public ActionResult Logout()
        {
            Request.GetOwinContext()
       .Authentication
       .SignOut(HttpContext.GetOwinContext()
                           .Authentication.GetAuthenticationTypes()
                           .Select(o => o.AuthenticationType).ToArray());
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}


namespace Tarificateur.Controllers.Api
{
    [System.Web.Http.Authorize]
    public class AccountController : ApiController
    {
        /// <summary>
        /// Permet de se loguer à l'api
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Description.ResponseType(typeof(Business.Common.Tools.LoginResult))]
        public HttpResponseMessage Login(string email, string password)
        {
            HttpResponseMessage response = null;
            if (ModelState.IsValid)
            {
                var existingUser = Business.Lists.Db.Instance.Users.FirstOrDefault(u => u.Email == email);

                if (existingUser == null)
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound);
                }
                else
                {
                    var loginSuccess =
                        string.Equals(Business.Common.Tools.EncryptPassword(password, existingUser.Salt),
                            existingUser.PasswordHash);

                    if (loginSuccess)
                    {
                        var tokenResult = Business.Common.Tools.CreateToken(existingUser);
                        response = Request.CreateResponse(tokenResult);
                    }
                }
            }
            else
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            return response;
        }


    }
}
