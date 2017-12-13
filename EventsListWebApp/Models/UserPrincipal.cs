using System.Linq;
using System.Security.Principal;

namespace EventsListWebApp.Models
{
    public class UserPrincipal : IPrincipal
    {
        public int UserId { get; }
        public string UserName { get; set; }
        public string[] Roles { get; set; }
        public IIdentity Identity
        {
            get; private set;
        }

        public UserPrincipal(string userName,int userId)
        {
            Identity = new WindowsIdentity(userName);
            UserId = userId;
        }

        public bool IsInRole(string role)
        {
            return Roles.Contains(role);
        }

    }
}