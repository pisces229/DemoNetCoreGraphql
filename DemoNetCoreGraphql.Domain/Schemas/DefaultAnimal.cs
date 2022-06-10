using GraphQL;
using GraphQL.Instrumentation;
using GraphQL.Types;

namespace DemoNetCoreGraphql.Domain.Schemas.DefaultAnimal
{
    public class DefaultAnimalSchema : Schema
    {
        public DefaultAnimalSchema(DefaultAnimalQuery query, DefaultAnimalMutation mutation)
        {
            Query = query;
            Mutation = mutation;
            //RegisterType(typeof(GraphType));
            // Middleware required for Apollo tracing to record performance metrics of field resolvers.
            FieldMiddleware.Use(new InstrumentFieldsMiddleware());
        }
    }
    public class DefaultAnimalQuery : ObjectGraphType
    {
        public DefaultAnimalQuery()
        {
            Name = "Query";
            Description = "This is DefaultAnimalQuery.";
            Field<BooleanGraphType>(name: "run", resolve: (context) => true);
            #region Scalars
            Field<DefaultAnimalInterfaceGraphType>(
                name: "animal",
                resolve: context => {
                    return new DefaultDog()
                    {
                        Id = "0",
                        Name = "Dog",
                        //DogName = "DogName"
                    };
                });
            Field<DefaultDogGraphType>(
                name: "dog",
                resolve: context =>
                {
                    return new DefaultDog()
                    {
                        Id = "0",
                        Name = "Dog",
                        DogName = "DogName"
                    };
                });
            Field<DefaultCatGraphType>(
                name: "cat",
                resolve: context =>
                {
                    return new DefaultCat()
                    {
                        Id = "0",
                        Name = "Cat",
                        CatName = "CatName"
                    };
                });
            #endregion

            #region List
            Field<ListGraphType<DefaultAnimalInterfaceGraphType>>(
                name: "animals",
                resolve: context => {
                    return new List<DefaultAnimal>()
                    {
                        new DefaultDog()
                        {
                            Id = "1",
                            Name = "Dog",
                            DogName = "DogName"
                        },
                        new DefaultCat()
                        {
                            Id = "2",
                            Name = "Cat",
                            CatName = "CatName"
                        }
                    };
                });
            Field<ListGraphType<DefaultDogGraphType>>(
                name: "dogs",
                resolve: context =>
                {
                    return new List<DefaultDog>()
                    {
                        new DefaultDog()
                        {
                            Id = "0",
                            Name = "Dog[0]",
                            DogName = "DogName[0]"
                        },
                        new DefaultDog()
                        {
                            Id = "1",
                            Name = "Dog[1]",
                            DogName = "DogName[1]"
                        },
                    };
                });
            Field<ListGraphType<DefaultCatGraphType>>(
                name: "cats",
                resolve: context =>
                {
                    return new List<DefaultCat>()
                    {
                        new DefaultCat()
                        {
                            Id = "0",
                            Name = "Dog[0]",
                            CatName = "CatName[0]"
                        },
                        new DefaultCat()
                        {
                            Id = "1",
                            Name = "Dog[1]",
                            CatName = "CatName[1]"
                        },
                    };
                });
            #endregion

        }
    }
    public class DefaultAnimalMutation : ObjectGraphType
    {
        public DefaultAnimalMutation()
        {
            Name = "Mutation";
            Description = "This is DefaultAnimalMutation.";
            Field<BooleanGraphType>(name: "run", resolve: (context) => true);
        }
    }

    #region GraphType
    public class DefaultAnimal
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
    }
    public class DefaultDog : DefaultAnimal
    {
        public string? DogName { get; set; }
    }
    public class DefaultCat : DefaultAnimal
    {
        public string? CatName { get; set; }
    }
    public class DefaultAnimalInterfaceGraphType : InterfaceGraphType<DefaultAnimal>
    {
        public DefaultAnimalInterfaceGraphType()
        {
            Name = "Animal";
            Description = "This is Animal.";
            Field(d => d.Id).Description("This is Id.");
            Field(d => d.Name).Description("This is Name.");
        }
    }
    public class DefaultDogGraphType : ObjectGraphType<DefaultDog>
    {
        public DefaultDogGraphType()
        {
            Name = "Dog";
            Description = "This is Dog.";
            Field(d => d.Id).Description("This is Id.");
            Field(d => d.Name).Description("This is Name.");
            Field(d => d.DogName, nullable: true).Description("This is DogName.");
            Interface<DefaultAnimalInterfaceGraphType>();
            //IsTypeOf = o =>
            //{
            //    return o is DefaultDog;
            //};
        }
    }
    public class DefaultCatGraphType : ObjectGraphType<DefaultCat>
    {
        public DefaultCatGraphType()
        {
            Name = "Cat";
            Description = "This is Cat.";
            Field(d => d.Id).Description("This is Id.");
            Field(d => d.Name).Description("This is Name.");
            Field(d => d.CatName, nullable: true).Description("This is CatName.");
            Interface<DefaultAnimalInterfaceGraphType>();
            //IsTypeOf = o =>
            //{
            //    return o is DefaultCat;
            //};
        }
    }
    #endregion
}
