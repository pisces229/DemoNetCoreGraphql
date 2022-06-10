using GraphQL;
using GraphQL.Instrumentation;
using GraphQL.Types;

namespace DemoNetCoreGraphql.Domain.Schemas.Default
{
    public class DefaultSchema : Schema
    {
        public DefaultSchema(DefaultQuery query, DefaultMutation mutation)
        {
            Query = query;
            Mutation = mutation;
            //RegisterType(typeof(GraphType));
            // Middleware required for Apollo tracing to record performance metrics of field resolvers.
            FieldMiddleware.Use(new InstrumentFieldsMiddleware());
        }
    }
    public class DefaultQuery : ObjectGraphType
    {
        public DefaultQuery()
        {
            Name = "Query";
            Description = "This is DefaultQuery.";
            Field<BooleanGraphType>(name: "run", resolve: (context) => true);
            // Field / FieldAsync
            //Field<NonNullGraphType<GraphType>>(
            //    name: "key",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<GraphType>> { Name = "key", Description = "Description." }
            //    ),
            //    resolve: (context) => result);
            //FieldAsync<NonNullGraphType<<GraphType>>(
            //    name: "key",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<GraphType>> { Name = "key", Description = "Description." }
            //    ),
            //    resolve: async (context) => await Task.FromResult(result));
            #region Scalars
            Field<BooleanGraphType>(
                name: "boolean",
                arguments: new QueryArguments(
                    new QueryArgument<BooleanGraphType> { Name = "input", Description = "This is input." }
                ),
                resolve: (context) => context.GetArgument<bool?>("input"));
            Field<StringGraphType>(
                name: "string",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "input", Description = "This is input." }
                ),
                resolve: (context) => context.GetArgument<string?>("input"));
            Field<IntGraphType>(
                name: "int",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "input", Description = "This is input." }
                ),
                resolve: (context) => context.GetArgument<int?>("input"));
            Field<DateTimeGraphType>(
                name: "datetime",
                arguments: new QueryArguments(
                    new QueryArgument<DateTimeGraphType> { Name = "input", Description = "This is input." }
                ),
                resolve: (context) => context.GetArgument<DateTime?>("input"));
            Field<DefaultFirstGraphType>(
                name: "first",
                arguments: new QueryArguments(
                    new QueryArgument<DefaultFirstInputGraphType> { Name = "input", Description = "This is input." }
                ),
                resolve: (context) => {
                    return context.GetArgument<DefaultFirst?>("input");
                });
            #endregion

            #region List
            Field<ListGraphType<BooleanGraphType>>(
                name: "booleans",
                arguments: new QueryArguments(
                    new QueryArgument<ListGraphType<BooleanGraphType>> { Name = "input", Description = "This is input." }
                ),
                resolve: (context) => context.GetArgument<List<bool>?>("input"));
            Field<ListGraphType<StringGraphType>>(
                name: "strings",
                arguments: new QueryArguments(
                    new QueryArgument<ListGraphType<StringGraphType>> { Name = "input", Description = "This is input." }
                ),
                resolve: (context) => context.GetArgument<List<string>?>("input"));
            Field<ListGraphType<IntGraphType>>(
                name: "ints",
                arguments: new QueryArguments(
                    new QueryArgument<ListGraphType<IntGraphType>> { Name = "input", Description = "This is input." }
                ),
                resolve: (context) => context.GetArgument<List<int>?>("input"));
            Field<ListGraphType<DateTimeGraphType>>(
                name: "datetimes",
                arguments: new QueryArguments(
                    new QueryArgument<ListGraphType<DateTimeGraphType>> { Name = "input", Description = "This is input." }
                ),
                resolve: (context) => context.GetArgument<List<DateTime>?>("input"));
            Field<ListGraphType<DefaultFirstGraphType>>(
                name: "firsts",
                arguments: new QueryArguments(
                    new QueryArgument<ListGraphType<DefaultFirstInputGraphType>> { Name = "input", Description = "This is input." }
                ),
                resolve: (context) => context.GetArgument<List<DefaultFirst>?>("input"));
            #endregion
        }
    }
    public class DefaultMutation : ObjectGraphType
    {
        public DefaultMutation()
        {
            Name = "Mutation";
            Description = "This is DefaultMutation.";
            Field<BooleanGraphType>(name: "run", resolve: (context) => true);
            Field<NonNullGraphType<DefaultFirstGraphType>>(
                name: "first",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<DefaultFirstInputGraphType>> { Name = "input", Description = "This is input." }
                ),
                resolve: (context) =>
                {
                    return context.GetArgument<DefaultFirst>("input");
                });
        }
    }

    #region GraphType
    public class DefaultFirst
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Date { get; set; }
    }
    public class DefaultFirstGraphType : ObjectGraphType<DefaultFirst>
    {
        public DefaultFirstGraphType()
        {
            Name = "First";
            Description = "This is First.";
            Field(h => h.Id).Description("This is Id.");
            Field(h => h.Name).Description("This is Name.");
            Field(h => h.Date, nullable: true).Description("This is Date.");
            Field<DefaultSecondGraphType>(
              name: "second",
              resolve: (context) => {
                  //context.Errors.Add(new ExecutionError("Error Message"));
                  var source = context.Source;
                  return new DefaultSecond()
                  {
                      Id = source.Id,
                      Name = source.Name,
                      Date = DateTime.Now,
                  };
              }
            );
            Field<ListGraphType<DefaultSecondGraphType>>(
              name: "seconds",
              resolve: (context) => {
                  //context.Errors.Add(new ExecutionError("Error Message"));
                  var source = context.Source;
                  return new List<DefaultSecond>()
                  {
                      new DefaultSecond()
                      {
                          Id = source.Id,
                          Name = source.Name,
                          Date = DateTime.Now,
                      },
                      new DefaultSecond()
                      {
                          Id = source.Id,
                          Name = source.Name,
                          Date = DateTime.Now,
                      }
                  };
              }
            );
        }
    }
    public class DefaultFirstInputGraphType : InputObjectGraphType<DefaultFirst>
    {
        public DefaultFirstInputGraphType()
        {
            Name = "FirstInput";
            Description = "This is FirstInput.";
            //Field<NonNullGraphType<StringGraphType>>(name: "name", description: "description");
            Field< NonNullGraphType<IntGraphType>>(name: "id", description: "This is Id.");
            Field<StringGraphType>(name: "name", description: "This is Name.");
            // date: 2016-07-20T17:30:15+05:30
            Field<DateTimeGraphType>(name: "date", description: "This is Date.");
        }
    }
    public class DefaultSecond
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Date { get; set; }
    }
    public class DefaultSecondGraphType : ObjectGraphType<DefaultSecond>
    {
        public DefaultSecondGraphType()
        {
            Name = "Second";
            Description = "This is Second.";
            Field(h => h.Id).Description("This is Id.");
            Field(h => h.Name).Description("This is Name.");
            Field(h => h.Date, nullable: true).Description("This is Date.");
        }
    }
    #endregion
}
