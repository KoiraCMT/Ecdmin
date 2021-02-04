using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecdmin.Core.Entities.Admin
{
    public class Role : EntityBase, IEntityTypeBuilder<Role>
    {
        public Role()
        {
            RolePermissions = new List<RolePermission>();
        }

        [Required, MaxLength(50)]
        public string Name { get; set; }
        
        [Required, MaxLength(50)]
        public string DisplayName { get; set; }
        
        public string? Description { get; set; }
        
        public virtual DateTimeOffset CreatedTime { get; set; }

        /// <summary>更新时间</summary>
        public virtual DateTimeOffset? UpdatedTime { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }
        
        public void Configure(EntityTypeBuilder<Role> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("role");
        }
    }
}