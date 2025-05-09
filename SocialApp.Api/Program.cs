using SocialApp.Api.Extensions;
using SocialApp.Api.Extensions.AuthPolicy;
using SocialApp.Api.Extensions.Configurations;
using SocialApp.Api.Extensions.Middleware;
using SocialApp.Api.Extensions.Swagger;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddConfigurations(builder.Configuration);

builder.Services.AddGenericRepository();

builder.Services.AddAuth(builder.Configuration);
builder.Services.AddLoggingConfiguration();
builder.Services.AddSwaggerConfiguration();

builder.Services.AddAppModules(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new GlobalRoutePrefixConvention("api/social_app"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAll");
}

app.UseMiddleware();

await app.Services.UseAppModules();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();