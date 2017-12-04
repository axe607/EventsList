using System.Web;
using System.Web.Mvc;

namespace EventsListWebApp.Models
{
    public class AdminAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = HttpContext.Current.User as UserPrincipal;
            if (user == null || !user.IsInRole("Admin"))
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Access", action = "Denied" }));
            }

        }
    }
}