using IdentityServer;
using IdentityServer4.Models;
using IdentityServer4.Test;

var builder = WebApplication.CreateBuilder(args);

//Config IdentityServer4
builder.Services.AddIdentityServer()
    .AddInMemoryClients(Config.Clients)
    //.AddInMemoryIdentityResources(Config.IdentityResources)
    //.AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
  //  .AddTestUsers(Config.TestUsers)
    .AddDeveloperSigningCredential();


var app = builder.Build();


//inject Identity Server
app.UseIdentityServer();
app.MapGet("/", () => "Hello World!");

app.Run();
