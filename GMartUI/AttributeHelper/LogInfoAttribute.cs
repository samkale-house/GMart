using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GMartUI.AttributeHelper
{
    public class LogInfoAttribute : ActionFilterAttribute
    {
        //executes before the action method executes
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string message="";
            if (context != null)
            {
                string controllerName = ((Microsoft.AspNetCore.Mvc.ControllerBase)context.Controller).ControllerContext.ActionDescriptor.ControllerName;
                string actionName = ((Microsoft.AspNetCore.Mvc.ControllerBase)context.Controller).ControllerContext.ActionDescriptor.ActionName;


                message = $"In controller:{controllerName}, In Action:{actionName}";
            }

            Debug.WriteLine(message,$"Action Filter Logs at {DateTime.Now.ToString()}");
        }

    }
}