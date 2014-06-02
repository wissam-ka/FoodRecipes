using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using System.Diagnostics;


namespace FoodRecipes.Filters
{
    public class LogginActionFilter
    {
        public class ExceptionLoggingActionFiter : HandleErrorAttribute
        {
            private static readonly ILog _log = log4net.LogManager.GetLogger(typeof(Controller));
            public override void OnException(ExceptionContext filterContext)
            {

                _log.DebugFormat("Errory at {0}", filterContext.Exception.Source);
                _log.Error(filterContext.Exception.Message, filterContext.Exception);


                base.OnException(filterContext);
            }
        }

        public class LoggingActionFilter : ActionFilterAttribute, IActionFilter, IResultFilter
        {

            private static readonly ILog _log = log4net.LogManager.GetLogger(typeof(Controller));
            void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
            {
                var controllervar = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                _log.Debug(controllervar);

                _log.DebugFormat("Entering {0} (Logged By: CustomAction Filter)", filterContext.ActionDescriptor.ActionName);
                _log.Debug(filterContext.HttpContext.Timestamp.ToLongTimeString());

                filterContext.HttpContext.Items["stopwatch"] = Stopwatch.StartNew();

                this.OnActionExecuting(filterContext);
            }

            void IResultFilter.OnResultExecuted(ResultExecutedContext filterContext)
            {
                _log.DebugFormat("Leaving {0} ", filterContext.HttpContext.Timestamp.ToLongTimeString());
                this.OnResultExecuted(filterContext);
            }

            public override void OnActionExecuted(ActionExecutedContext filterContext)
            {
                Stopwatch stopwatch = (Stopwatch)filterContext.HttpContext.Items["stopwatch"];
                 stopwatch.Stop();
                _log.DebugFormat("action executed{0}",stopwatch);
                base.OnActionExecuted(filterContext);
            }
        }
    }
}