using Common.Configurations;
using SocialNetworkAccounts.Application;
using SocialNetworkAccounts.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.AddAuth();
builder.AddSocialNetworkAccountsApplication();
builder.AddSocialNetworkAccountsPersistence();
builder.AddSwaggerConfiguration();
builder.AddLoggingConfiguration();
builder.Services.AddCors(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyOrigin() 
                .AllowAnyHeader() 
                .AllowAnyMethod(); 
        });
    }
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
    app.UseCors("AllowAll");
}

app.UseSocialNetworkAccountsPersistence();
app.UseHttpsRedirection();
app.UseMiddleware();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();
