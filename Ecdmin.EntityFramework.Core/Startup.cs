using Ecdmin.EntityFramework.Core.DbContexts;
using Furion;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecdmin.EntityFramework.Core
{
    public class Startup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseAccessor(options =>
            {
                options.AddDbPool<SqlServerDbContext>(DbProvider.SqlServer, options =>
                {
                    var cs = App.Configuration["ConnectionStrings:DbConnectionString"];
                    options.UseSqlServer(cs, 
                        b => b.MigrationsAssembly("Ecdmin.Database.Migrations")
                    );
                });
            });
        }
    }
}