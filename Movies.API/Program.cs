using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Movies.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*builder.Services.AddDbContext<MoviesContext>(options =>
             options.UseNpgsql(builder.Configuration.GetConnectionString("MoviesAPIContext")));
*/
/// <summary>
/// / generate token to client api
/// </summary>

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
     {
         options.Authority = "https://localhost:5005";/// IdentityServer4
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateAudience = false
     };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ClientIdPolicy", policy => policy.RequireClaim("client_id", "movieClient", "movies_mvc_client"));
});



builder.Services.AddDbContext<MoviesContext>(options =>
             options.UseInMemoryDatabase("MoviesAPIContext"));

var app = builder.Build();
// seeding data to database automatique 
using var scope = app.Services.CreateScope();
using var context = scope.ServiceProvider.GetService<MoviesContext>();
context.Database.EnsureCreated();
MoviesContextSeed.SeedAsync(context);
//end seeding data to database 


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
