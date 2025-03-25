using System.Text;
using Common.Configurations;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot();
builder.AddAuth();
builder.AddLoggingConfiguration();

var app = builder.Build();

await app.UseOcelot();
app.UseMiddleware<JsonBodyMiddleware>();
app.UseAuthentication();
app.UseAuthorization();


app.Run();