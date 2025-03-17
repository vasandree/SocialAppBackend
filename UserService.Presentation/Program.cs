using Common.Configurations;
using UserService.Application;
using UserService.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.AddUserServicePersistence();
builder.AddAuth();
builder.AddSwaggerConfiguration();
builder.AddUserServiceApplication();
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

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
}

app.UseUserServicePersistence();
app.UseHttpsRedirection();
app.UseMiddleware();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();