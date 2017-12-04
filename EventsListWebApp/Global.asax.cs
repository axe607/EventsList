using EventsListCommon.Models;
using EventsListWebApp.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace EventsListWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        public void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var auth = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (auth != null)
            {
                var ticket = FormsAuthentication.Decrypt(auth.Value);
                var model = JsonConvert.DeserializeObject<User>(ticket.UserData);
                var principal = new UserPrincipal(ticket.Name)
                {
                    UserName = model.UserName,
                    Roles = model.Roles.Select(x => x.RoleName).ToArray()
                };
                HttpContext.Current.User = principal;
            }
        }
    }
}
