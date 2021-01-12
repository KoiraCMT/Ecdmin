using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace Ecdmin.EntityFramework.Core.DbContexts
{
    public class SqlServerDbContext : AppDbContext<SqlServerDbContext>
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
        {
        }
    }
}