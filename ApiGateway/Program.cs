using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


// ocelot
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
	config.AddJsonFile($"ocelot.json", false, true);
});
var authenticationProviderKey = "IdentityApiKey";

// NUGET - Microsoft.AspNetCore.Authentication.JwtBearer
builder.Services.AddAuthentication()
 .AddJwtBearer(authenticationProviderKey, x =>
 {
     x.Authority = "https://localhost:5005"; // IDENTITY SERVER URL
                                             //x.RequireHttpsMetadata = false;
                 x.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateAudience = false
     };
 });
builder.Services
	.AddOcelot();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
	endpoints.MapGet("/", async context =>
	{
		await context.Response.WriteAsync("Ocelot API Gateway Up!");
	});
});

app.UseOcelot();

app.Run();