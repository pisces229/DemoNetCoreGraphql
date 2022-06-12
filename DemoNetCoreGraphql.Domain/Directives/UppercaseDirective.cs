using GraphQL.Types;
using GraphQLParser.AST;

namespace DemoNetCoreGraphql.Domain.Directives
{
    public class UppercaseDirective : Directive
    {
        public UppercaseDirective()
            : base("uppercase", DirectiveLocation.FieldDefinition)
        {
            Description = "Converts the value of string fields to uppercase.";
        }
    }
}
