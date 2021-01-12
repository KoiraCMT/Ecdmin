using System;
using System.ComponentModel.DataAnnotations.Schema;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecdmin.Core.Entities.Admin
{
    public class RolePermission : IEntity, IEntityTypeBuilder<RolePermission>
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        
        public virtual Role Role { get; set; }
        
        public virtual Permission Permission { get; set; }
        
        public void Configure(EntityTypeBuilder<RolePermission> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("role_permission");
            entityBuilder.HasKey(t => new {t.RoleId, t.PermissionId});
        }
    }
}