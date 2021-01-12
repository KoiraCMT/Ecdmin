using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecdmin.Core.Entities.Admin
{
    public class PermissionGroup : EntityBase, IEntityTypeBuilder<PermissionGroup>
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
        
        public virtual List<Permission> Permissions { get; set; }
        public void Configure(EntityTypeBuilder<PermissionGroup> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("permission_group");
        }
    }
}