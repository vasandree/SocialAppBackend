using System.Reflection;
using Auth.Controllers;
using Common.Configurations;
using User.Controllers;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddMediatR(config
    => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.AddGenericRepository();

builder.AddUserModule();
builder.AddAuthModule();

var app = builder.Build();

app.UseUserModule();
app.UseAuthModule();

app.Run();