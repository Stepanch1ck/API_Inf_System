using RegistryOfInformationSystems.Database;
using RegistryOfInformationSystems.Interfaces;
using RegistryOfInformationSystems.Services;
using RegistryOfInformationSystems.Models;
using Microsoft.OpenApi.Models;

namespace RegistryOfInformationSystems.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddHttpContextAccessor();
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling 
                = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
           return services;
        }

        public static IServiceCollection ConfigureServicesDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<PostgresDbContext>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IInfSystemService, InfSystemService>();
            services.AddScoped<IAttributeService, AttributeService>();

            services.Configure<JwtTokenOptions>(configuration.GetSection(JwtTokenOptions.Jwt));
            services.AddScoped<JwtAuthService>();
            services.AddScoped<BasicAuthService>();


            return services;
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Test API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type=ReferenceType.SecurityScheme, Id="Bearer" }
                        }, 
                        new string[]{}
                    }
                });
            });

            return services;
        }
    }
}