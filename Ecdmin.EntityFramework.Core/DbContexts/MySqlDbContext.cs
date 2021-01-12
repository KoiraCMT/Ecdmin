using Ecdmin.Core.Entities;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace Ecdmin.EntityFramework.Core.DbContexts
{
    public class MySqlDbContext : AppDbContext<MySqlDbContext>
    {
        public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options)
        {
        }
    }
}