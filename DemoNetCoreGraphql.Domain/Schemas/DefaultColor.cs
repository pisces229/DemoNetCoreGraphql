using GraphQL;
using GraphQL.Instrumentation;
using GraphQL.Types;

namespace DemoNetCoreGraphql.Domain.Schemas.DefaultColor
{
    public class DefaultColorSchema : Schema
    {
        public DefaultColorSchema(DefaultColorQuery query, DefaultColorMutation mutation)
        {
            Query = query;
            Mutation = mutation;
            //RegisterType(typeof(GraphType));
            // Middleware required for Apollo tracing to record performance metrics of field resolvers.
            FieldMiddleware.Use(new InstrumentFieldsMiddleware());
        }
    }
    public class DefaultColorQuery : ObjectGraphType
    {
        public DefaultColorQuery()
        {
            Name = "Query";
            Description = "This is DefaultColorQuery.";
            Field<BooleanGraphType>(name: "run", resolve: (context) => true);

            Field<DefaultColorGraphType>(
                name: "red", 
                resolve: (context) => new DefaultRed()
                {
                    Name = "Red",
                    Red = 0,
                });
            Field<DefaultColorGraphType>(
                name: "green",
                resolve: (context) => new DefaultGreen()
                {
                    Name = "Green",
                    Green = 1,
                });
            Field<DefaultColorGraphType>(
                name: "bule",
                resolve: (context) => new DefaultBlue()
                {
                    Name = "Blue",
                    Blue = 0,
                });
            Field<ListGraphType<DefaultColorGraphType>>(
                name: "color",
                resolve: (context) => new List<object>()
                {
                    new DefaultRed()
                    {
                        Name = "Red",
                        Red = 0,
                    },
                    new DefaultGreen()
                    {
                        Name = "Green",
                        Green = 1,
                    },
                    new DefaultBlue()
                    {
                        Name = "Blue",
                        Blue = 0,
                    }
                });
        }
    }
    public class DefaultColorMutation : ObjectGraphType
    {
        public DefaultColorMutation()
        {
            Name = "Mutation";
            Description = "This is DefaultColorMutation.";
            Field<BooleanGraphType>(name: "run", resolve: (context) => true);
        }
    }

    #region GraphType
    public class DefaultRed
    {
        public string? Name { get; set; }
        public int Red { get; set; }
    }
    public class DefaultGreen
    {
        public string? Name { get; set; }
        public int Green { get; set; }
    }
    public class DefaultBlue
    {
        public string? Name { get; set; }
        public int Blue { get; set; }
    }
    public class DefaultRedGraphType : ObjectGraphType<DefaultRed>
    {
        public DefaultRedGraphType()
        {
            Name = "Red";
            Description = "This is Red.";
            Field(d => d.Name).Description("This is Name.");
            Field(d => d.Red).Description("This is Red.");
            //IsTypeOf = o =>
            //{
            //    return o is DefaultRed;
            //};
        }
    }
    public class DefaultGreenGraphType : ObjectGraphType<DefaultGreen>
    {
        public DefaultGreenGraphType()
        {
            Name = "Green";
            Description = "This is Green.";
            Field(d => d.Name).Description("This is Name.");
            Field(d => d.Green).Description("This is Green.");
            //IsTypeOf = o => 
            //{
            //    return o is DefaultGreen;
            //};
        }
    }
    public class DefaultBlueGraphType : ObjectGraphType<DefaultBlue>
    {
        public DefaultBlueGraphType()
        {
            Name = "Blue";
            Description = "This is Blue.";
            Field(d => d.Name).Description("This is Name.");
            Field(d => d.Blue).Description("This is Blue.");
            //IsTypeOf = o => 
            //{
            //    return o is DefaultBlue;
            //};
        }
    }
    public class DefaultColorGraphType : UnionGraphType
    {
        public DefaultColorGraphType()
        {
            Type<DefaultRedGraphType>();
            Type<DefaultGreenGraphType>();
            Type<DefaultBlueGraphType>();
        }
    }
    #endregion
}
