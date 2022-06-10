using DemoNetCoreGraphql.Domain.Sample;
using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.SystemTextJson;
using System.Security.Claims;

#region WebApplicationBuilder
var webApplicationBuilder = WebApplication.CreateBuilder(args);
webApplicationBuilder.Services.AddGraphQL(builder => builder
    .AddSchema<StarWarsSchema>()
    .AddSystemTextJson()
    .AddValidationRule<InputValidationRule>()
    .AddGraphTypes(typeof(StarWarsSchema).Assembly));
webApplicationBuilder.Services.AddSingleton<StarWarsData>();
webApplicationBuilder.Services.AddLogging(builder => builder.AddConsole());
webApplicationBuilder.Services.AddHttpContextAccessor();
webApplicationBuilder.Services.AddControllersWithViews()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new InputsJsonConverter());
    });
#endregion

#region WebApplication
var webApplication = webApplicationBuilder.Build();
if (webApplication.Environment.IsDevelopment())
{
    webApplication.UseDeveloperExceptionPage();
}
webApplication.UseRouting();
webApplication.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
webApplication.UseDefaultFiles();
webApplication.UseStaticFiles();
webApplication.Run();
#endregion