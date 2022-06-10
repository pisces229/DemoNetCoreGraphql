using System.Security.Claims;

namespace DemoNetCoreGraphql.Graphiql
{
    public class GraphQLUserContext : Dictionary<string, object?>
    {
        public ClaimsPrincipal? User { get; set; }
    }
}
