using DemoNetCoreGraphql.Domain;
using DemoNetCoreGraphql.Domain.Sample;
using DemoNetCoreGraphql.Domain.Schemas.Default;
using DemoNetCoreGraphql.Domain.Schemas.DefaultAnimal;
using DemoNetCoreGraphql.Domain.Schemas.DefaultColor;
using DemoNetCoreGraphql.Domain.ValidationRules;
using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.SystemTextJson;
using GraphQL.Validation.Complexity;

#region WebApplicationBuilder
var webApplicationBuilder = WebApplication.CreateBuilder(args);
webApplicationBuilder.Services.AddGraphQL(builder => 
builder
    .AddHttpMiddleware<StarWarsSchema>()
    .AddHttpMiddleware<DefaultSchema>()
    .AddHttpMiddleware<DefaultAnimalSchema>()
    .AddHttpMiddleware<DefaultColorSchema>()
    .AddUserContextBuilder(httpContext => 
    {
        var userContext = new DefaultUserContext
        { 
            User = httpContext.User,
            IsAdmin = true,
        };
        //userContext.Add("key", "value");
        //var method = httpContext.Request.Method;
        return userContext;
    })
    .AddSchema<StarWarsSchema>()
    .AddSchema<DefaultSchema>()
    .AddSchema<DefaultAnimalSchema>()
    .AddSchema<DefaultColorSchema>()
    .AddValidationRule<DefaultValidationRule>()
    .AddSystemTextJson()
    .ConfigureExecutionOptions(option =>
    {
        //option.ComplexityConfiguration = new ComplexityConfiguration { MaxDepth = 15 };
        //option.UnhandledExceptionDelegate = async context =>
        //{
        //    await Task.FromResult("");
        //};
    })
    .AddErrorInfoProvider(options => options.ExposeExceptionStackTrace = true)
    .AddGraphTypes(typeof(LoadGraphType).Assembly));
webApplicationBuilder.Services.AddSingleton<StarWarsData>();
webApplicationBuilder.Services.AddLogging(builder => builder.AddConsole());
webApplicationBuilder.Services.AddHttpContextAccessor();
#endregion

#region WebApplication
var webApplication = webApplicationBuilder.Build();
if (webApplication.Environment.IsDevelopment())
{
    webApplication.UseDeveloperExceptionPage();
}
webApplication.UseGraphQL<StarWarsSchema>("/api/sample");
webApplication.UseGraphQL<DefaultSchema>("/api/default");
webApplication.UseGraphQL<DefaultAnimalSchema>("/api/defaultAnimal");
webApplication.UseGraphQL<DefaultColorSchema>("/api/defaultColor");
webApplication.UseGraphQLPlayground(
    new PlaygroundOptions { GraphQLEndPoint = "/api/sample" }, "/ui/sample");
webApplication.UseGraphQLPlayground(
    new PlaygroundOptions { GraphQLEndPoint = "/api/default" }, "/ui/default");
webApplication.UseGraphQLPlayground(
    new PlaygroundOptions { GraphQLEndPoint = "/api/defaultAnimal" }, "/ui/defaultAnimal");
webApplication.UseGraphQLPlayground(
    new PlaygroundOptions { GraphQLEndPoint = "/api/defaultColor" }, "/ui/defaultColor");
webApplication.UseGraphQLAltair("/ui/altair");
webApplication.Run();
#endregion