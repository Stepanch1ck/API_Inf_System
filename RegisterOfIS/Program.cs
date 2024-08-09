using RegisterOfIS.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureControllers()
    .ConfigureSwagger()
    .ConfigureServicesDI(builder.Configuration)
    .ConfigureJwtAuthentication(builder.Configuration)
    .ConfigureAuthorizationPolicies()
    .ConfigureCors();

var app = builder.Build();

app.ConfigureWeb();

app.Run();
