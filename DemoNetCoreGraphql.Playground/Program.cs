using DemoNetCoreGraphql.Domain;
using DemoNetCoreGraphql.Domain.Sample;
using DemoNetCoreGraphql.Domain.Schemas.Default;
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
    .AddUserContextBuilder(context => new GraphQLUserContext { User = context.User })
    .AddSchema<StarWarsSchema>()
    .AddSchema<DefaultSchema>()
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
webApplication.UseGraphQLPlayground(new PlaygroundOptions { GraphQLEndPoint = "/api/sample" }, "/ui/sample");
webApplication.UseGraphQLPlayground(new PlaygroundOptions { GraphQLEndPoint = "/api/default" }, "/ui/default");
webApplication.UseGraphQLAltair("/ui/altair");
webApplication.Run();
#endregion