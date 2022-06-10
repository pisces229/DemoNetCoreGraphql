using System.Security.Claims;

namespace DemoNetCoreGraphql.Playground
{
    public class GraphQLUserContext : Dictionary<string, object>
    {
        public ClaimsPrincipal? User { get; set; }
    }
}
