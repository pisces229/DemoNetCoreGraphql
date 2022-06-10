using DemoNetCoreGraphql.Domain;
using DemoNetCoreGraphql.Domain.Sample;
using DemoNetCoreGraphql.Domain.Schemas.Default;
using DemoNetCoreGraphql.Domain.Schemas.DefaultAnimal;
using DemoNetCoreGraphql.Domain.Schemas.DefaultColor;
using DemoNetCoreGraphql.Playground;
using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.SystemTextJson;

#region WebApplicationBuilder
var webApplicationBuilder = WebApplication.CreateBuilder(args);
webApplicationBuilder.Services.AddGraphQL(builder => builder
    .AddHttpMiddleware<StarWarsSchema>()
    .AddHttpMiddleware<DefaultSchema>()
    .AddHttpMiddleware<DefaultAnimalSchema>()
    .AddHttpMiddleware<DefaultColorSchema>()
    .AddUserContextBuilder(context => new GraphQLUserContext { User = context.User })
    .AddSchema<StarWarsSchema>()
    .AddSchema<DefaultSchema>()
    .AddSchema<DefaultAnimalSchema>()
    .AddSchema<DefaultColorSchema>()
    .AddSystemTextJson()
    .AddErrorInfoProvider(options => options.ExposeExceptionStackTrace = false)
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