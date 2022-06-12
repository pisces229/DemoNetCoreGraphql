using GraphQL.Validation;
using GraphQLParser.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNetCoreGraphql.Domain.ValidationRules
{
    public class DefaultValidationRule : IValidationRule
    {
        public ValueTask<INodeVisitor?> ValidateAsync(ValidationContext validationContext)
        {
            //context.UserContext
            //var userContext = context.UserContext as GraphQLUserContext;
            //var authenticated = userContext.User?.IsAuthenticated() ?? false;

            return new ValueTask<INodeVisitor?>(new MatchingNodeVisitor<GraphQLField>(
                (field, context) => 
                {
                    Console.WriteLine("");
                },
                (field, context) => 
                {
                    Console.WriteLine("");
                }));

            //return Task.FromResult(new EnterLeaveListener(_ =>
            //{
            //    _.Match<Operation>(op =>
            //    {
            //        if (op.OperationType == OperationType.Mutation && !authenticated)
            //        {
            //            context.ReportError(new ValidationError(
            //                context.Document.Source,
            //                "6.1.1", // the rule number of this validation error corresponding to the paragraph number from the official specification
            //                $"Authorization is required to access {op.Name}.",
            //                op)
            //            { Code = "auth-required" });
            //        }
            //    });

            //    // this could leak info about hidden fields in error messages
            //    // it would be better to implement a filter on the schema so it
            //    // acts as if they just don't exist vs. an auth denied error
            //    // - filtering the schema is not currently supported
            //    _.Match<Field>(fieldAst =>
            //    {
            //        var fieldDef = context.TypeInfo.GetFieldDef();
            //        if (fieldDef.RequiresPermissions() &&
            //            (!authenticated || !fieldDef.CanAccess(userContext.User.Claims)))
            //        {
            //            context.ReportError(new ValidationError(
            //                context.Document.Source,
            //                "6.1.1", // the rule number of this validation error corresponding to the paragraph number from the official specification
            //                $"You are not authorized to run this query.",
            //                fieldAst)
            //            { Code = "auth-required" });
            //        }
            //    });
            //}));
        }
    }
}
