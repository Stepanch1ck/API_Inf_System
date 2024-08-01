using ReactApp2.Server.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<InfSystemDbContext>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
using var dbContext = scope.ServiceProvider.GetService<InfSystemDbContext>();
await dbContext.Database.EnsureCreatedAsync();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
