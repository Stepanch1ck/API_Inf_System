using Microsoft.EntityFrameworkCore;
using RegisterOfInformationSystems.Core.Interfaces;
using RegisterOfInformationSystems.Core.Models;
using RegisterOfInformationSystems.Database;
using RegisterOfInformationSystems.Service;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<PostgresDbContext>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<InformationSystemService>();
builder.Services.AddScoped<AttributeService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

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
using var dbContext = scope.ServiceProvider.GetService<PostgresDbContext>();

dbContext.Database.EnsureDeleted();

dbContext.Database.Migrate();



app.UseCors();
app.MapControllers();

app.Run();