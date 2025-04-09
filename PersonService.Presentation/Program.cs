using Common.Configurations;
using PersonService.Application;
using PersonService.Infrastructure;
using PersonService.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.AddAuth();
builder.AddInfrastructure();
builder.AddLoggingConfiguration();
builder.AddSwaggerConfiguration();
builder.AddPersonServiceApplication();
builder.AddPersonServicePersistence();
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

app.UseHttpsRedirection();
app.UseMiddleware();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.AddPersonServicePersistence();

app.Run();