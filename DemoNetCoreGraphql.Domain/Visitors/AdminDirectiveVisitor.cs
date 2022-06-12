using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Types;
using GraphQL.Utilities;

namespace DemoNetCoreGraphql.Domain.Visitors
{
    public class AdminDirectiveVisitor : BaseSchemaNodeVisitor //ISchemaNodeVisitor
    {
        public override void VisitObjectFieldDefinition(FieldType field, IObjectGraphType type, ISchema schema)
        {
            var appliedDirective = field.FindAppliedDirective("admin");
            if (appliedDirective != null)
            {
                var instance = field.Resolver ?? NameFieldResolver.Instance;
                field.Resolver = new FuncFieldResolver<object>(async context =>
                {
                    var defaultUserContext = context.UserContext as DefaultUserContext;
                    if (!defaultUserContext!.IsAdmin)
                    {
                        context.Errors.Add(new ExecutionError("Permission Denied."));
                        return null;
                    }
                    else
                    {
                        return await instance.ResolveAsync(context);
                    }
                });
            }
        }
    }
}
