using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Types;
using GraphQL.Utilities;

namespace DemoNetCoreGraphql.Domain.Visitors
{
    public class UppercaseDirectiveVisitor : BaseSchemaNodeVisitor //ISchemaNodeVisitor
    {
        public override void VisitObjectFieldDefinition(FieldType field, IObjectGraphType type, ISchema schema)
        {
            var appliedDirective = field.FindAppliedDirective("uppercase");
            if (appliedDirective != null)
            {
                //field.Description = "Description";
                var instance = field.Resolver ?? NameFieldResolver.Instance;
                field.Resolver = new FuncFieldResolver<object>(async context =>
                {
                    var value = await instance.ResolveAsync(context);
                    return value is string o ? o.ToUpperInvariant() : value;
                });
            }
        }
    }
}
