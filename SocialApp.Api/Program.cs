using Auth.Controllers;
using Shared.Configurations.Configurations;
using SocialNetworkAccounts.Controllers;
using SocialNode.Controllers;
using TaskModule.Controllers;
using User.Controllers;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddOpenApi();

builder.AddGenericRepository();

builder.AddAuth();
builder.AddLoggingConfiguration();
builder.AddSwaggerConfiguration();

builder.AddUserModule();
builder.AddAuthModule();
builder.AddSocialNetworkAccountsModule();
builder.AddSocialNodeModule();
builder.AddTaskModule();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
    app.UseCors("AllowAll");
}

app.UseMiddleware();

app.UseUserModule();
app.UseAuthModule();
app.UseSocialNetworkAccountsModule();
app.UseSocialNodeModule();
app.UseTaskModule();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();