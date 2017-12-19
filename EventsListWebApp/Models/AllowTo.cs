using System.Web;
using System.Web.Mvc;

namespace EventsListWebApp.Models
{
    public class AllowTo: AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = HttpContext.Current.User as UserPrincipal;
            if (user == null)
            {
                return false;
            }

            string[] roles = base.Roles.Split(',');
            foreach (string role in roles)
            {
                if (user.IsInRole(role))
                {
                    return true;
                }
            }
            return false;
        }
    }
}