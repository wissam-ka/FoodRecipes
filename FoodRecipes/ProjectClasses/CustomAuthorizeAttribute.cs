using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodRecipes.ProjectClasses
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");
            if (!httpContext.User.Identity.IsAuthenticated)
                return false;
            if ((httpContext.User.Identity.Name=="wk"))
                return false;
            return true;

        }
    }
}