using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecdmin.Core.Entities.Admin
{
    public class Permission : EntityBase, IEntityTypeBuilder<Permission>
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        public int PermissionGroupId { get; set; }
        
        [Required, MaxLength(50)]
        public string DisplayName { get; set; }

        public void Configure(EntityTypeBuilder<Permission> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("permission");
        }
    }
}