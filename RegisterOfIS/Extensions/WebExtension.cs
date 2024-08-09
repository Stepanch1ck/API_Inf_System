using RegisterOfIS.Database;
using Microsoft.EntityFrameworkCore;

namespace RegisterOfIS.Extensions
{
    public static class WebExtension
    {
        public static void ConfigureWeb(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            using var dbContext = scope.ServiceProvider.GetService<PostgresDbContext>();
            dbContext.Database.Migrate();
            dbContext.Database.EnsureCreated();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
