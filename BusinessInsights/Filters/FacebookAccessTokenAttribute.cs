using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BusinessInsights.Filters
{
    public class FacebookAccessTokenAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            ApplicationUserManager _userManager = filterContext.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (_userManager != null)
            {
                var claimsforUser = _userManager.GetClaimsAsync(filterContext.HttpContext.User.Identity.GetUserId());
                try
                {
                    var claim = claimsforUser.Result.FirstOrDefault(x => x.Type == "FacebookAccessToken");
                    if (claim != null)
                    {
                        var access_token = claim.Value;

                        if (filterContext.HttpContext.Items.Contains("access_token"))
                            filterContext.HttpContext.Items["access_token"] = access_token;
                        else
                            filterContext.HttpContext.Items.Add("access_token", access_token);
                    }
                    else
                    {
                        ForceLogout();
                    }
                }
                catch
                {
                    ForceLogout();
                }
            }
            base.OnActionExecuting(filterContext);
        }

        private void ForceLogout()
        {
            HttpContext.Current.Session.Abandon();
            FormsAuthentication.SignOut();
            HttpContext.Current.Response.Redirect("/Account/Login");
        }
    }
}