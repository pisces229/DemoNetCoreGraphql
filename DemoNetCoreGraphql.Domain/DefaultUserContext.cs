using System.Security.Claims;

namespace DemoNetCoreGraphql.Domain
{
    public class DefaultUserContext : Dictionary<string, object?>
    {
        public ClaimsPrincipal? User { get; set; }
        public bool IsAdmin { get; set; }
    }
}
