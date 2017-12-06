using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace EventsListWebApp.Models
{
    public class AjaxAttribute:ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return controllerContext.HttpContext.Request.IsAjaxRequest();
        }
    }
}