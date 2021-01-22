using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecdmin.Core.Entities.Admin
{
    public class Permission : EntityBase, IEntityTypeBuilder<Permission>
    {
        public Permission()
        {
            RolePermissions = new List<RolePermission>();
        }

        [Required, MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        public int PermissionGroupId { get; set; }
        
        [Required, MaxLength(50)]
        public string DisplayName { get; set; }
        
        [JsonIgnore]
        public ICollection<RolePermission> RolePermissions { get; set; }

        public void Configure(EntityTypeBuilder<Permission> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("permission");
        }
    }
}