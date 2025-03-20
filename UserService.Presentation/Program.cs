using Common.Configurations;
using UserService.Application;
using UserService.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.AddUserServicePersistence();
builder.AddAuth();
builder.AddSwaggerConfiguration();
builder.AddUserServiceApplication();
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

app.UseUserServicePersistence();
app.UseHttpsRedirection();
app.UseMiddleware();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();