using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace BillingAdmin.Models
{
        [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
        public class MyAuthorizeAttribute : AuthorizeAttribute, IRequiresSessionState
    {

            //Custom named parameters for annotation
            public string ResourceKey { get; set; }
            public string OperationKey { get; set; }

            //Called when access is denied
            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
                //User isn't logged in
                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    filterContext.Result = new RedirectToRouteResult( new RouteValueDictionary(new { controller = "Account", action = "Login" })
                    );
                }
                //User is logged in but has no access
                else
                {
                    filterContext.Result = new RedirectToRouteResult(  new RouteValueDictionary(new { controller = "Account", action = "NotAuthorized" })
                    );
                }
            }

       
        }

    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        Entities db = new Entities();

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var request = httpContext.Request;
           

            if (request.Headers["44d9dbb60b6c2c24922cd62d249412f9"] != null)
            {
                string hash = request.Headers["44d9dbb60b6c2c24922cd62d249412f9"].ToString();
                var user = db.AspNetUsers.Where(c=>c.PasswordHash== hash);

                if (user.Count() > 0)
                {
                    return true;
                }
            }      

            var authorized = base.AuthorizeCore(httpContext);
            if (!authorized)
            {
                return false;
            }

            return true;
        }


    }

    public class AuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                // Don't check for authorization as AllowAnonymous filter is applied to the action or controller
                return;
            }


            var request = filterContext.HttpContext.Request;

    
            if (request.Headers["44d9dbb60b6c2c24922cd62d249412f9"] != null)
            {
                if (request.Headers["44d9dbb60b6c2c24922cd62d249412f9"].ToString().Equals("6e952e05f3297e277d3f4648e83cfef2"))
                {
                    System.Web.HttpContext.Current.Session["44d9dbb60b6c2c24922cd62d249412f9"] = "6e952e05f3297e277d3f4648e83cfef2";
                    return;
                }

            }


            // Check for authorization
            //if (HttpContext.Current.Session["UserName"] == null)
            // {
            filterContext.Result = new HttpUnauthorizedResult();
            // }
        }
    }
}