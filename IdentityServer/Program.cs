using IdentityServer;
using IdentityServer4.Models;
using IdentityServer4.Test;

var builder = WebApplication.CreateBuilder(args);

//Config IdentityServer4
builder.Services.AddIdentityServer()
    .AddInMemoryClients(Config.Clients)
    .AddInMemoryIdentityResources(Config.IdentityResources)
    //.AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddTestUsers(Config.TestUsers)
    .AddDeveloperSigningCredential();
/// <summary>
/// add Controller And views
/// </summary>
builder.Services.AddControllersWithViews();


/// <summary>
///End add Controller And views
/// </summary>
var app = builder.Build();
//using static files like wwwroot
app.UseStaticFiles();
app.UseRouting();
//inject Identity Server
app.UseIdentityServer();
app.UseAuthorization();
//app.MapGet("/", () => "Hello World!");
app.MapDefaultControllerRoute();

app.Run();
