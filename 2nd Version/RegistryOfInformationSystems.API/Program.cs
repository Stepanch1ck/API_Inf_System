using Microsoft.EntityFrameworkCore;
using RegistryOfInformationSystems.Database;
using RegistryOfInformationSystems.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();


builder.Services
    .ConfigureControllers()
    .ConfigureSwagger()
    .ConfigureServicesDI(builder.Configuration)
    .ConfigureJwtAuthentication(builder.Configuration)
    .ConfigureAuthorizationPolicies()
    .ConfigureCors();

var app = builder.Build();

using var scope = app.Services.CreateScope();
using var dbContext = scope.ServiceProvider.GetService<PostgresDbContext>();
dbContext.Database.EnsureDeleted();
dbContext.Database.Migrate();
app.ConfigureWeb();

app.Run();
