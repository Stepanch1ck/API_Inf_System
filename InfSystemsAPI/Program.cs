using InfSystemsAPI.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<InfSystemAPIDbContext>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});


var app = builder.Build();

using var scope = app.Services.CreateScope();
using var dbContext = scope.ServiceProvider.GetService<InfSystemAPIDbContext>();
await dbContext.Database.EnsureDeletedAsync();
await dbContext.Database.EnsureCreatedAsync();


app.UseCors();
app.MapControllers();

app.Run();
