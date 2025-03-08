using Common.Configurations;
using UserService.Application;
using UserService.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.AddUserServicePersistence();
builder.AddAuth();
builder.AddSwaggerConfiguration();
builder.AddUserServiceApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerConfiguration();
}

app.ConfigureAuth();
app.UseHttpsRedirection();
app.UseRouting();

app.Run();