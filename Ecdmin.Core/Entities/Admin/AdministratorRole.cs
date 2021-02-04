using System;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecdmin.Core.Entities.Admin
{
    public class AdministratorRole : IEntity, IEntityTypeBuilder<AdministratorRole>
    {
        public int AdministratorId { get; set; }

        public int RoleId { get; set; }


        public virtual Administrator Administrator { get; set; }

        public virtual Role Role { get; set; }
        public void Configure(EntityTypeBuilder<AdministratorRole> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("administrator_role");
            entityBuilder.HasKey(t => new { t.AdministratorId, t.RoleId });
        }
    }
}