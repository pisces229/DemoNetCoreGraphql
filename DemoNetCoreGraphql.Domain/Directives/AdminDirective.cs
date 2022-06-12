using GraphQL.Types;
using GraphQLParser.AST;

namespace DemoNetCoreGraphql.Domain.Directives
{
    public class AdminDirective : Directive
    {
        public AdminDirective()
            : base("admin", DirectiveLocation.FieldDefinition)
        {
            Description = "Permission Denied.";
        }
    }
}
