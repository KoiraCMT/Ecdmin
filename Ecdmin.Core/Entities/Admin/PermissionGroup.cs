using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecdmin.Core.Entities.Admin
{
    public class PermissionGroup : EntityBase, IEntityTypeBuilder<PermissionGroup>, IEntitySeedData<PermissionGroup>
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
        
        public virtual List<Permission> Permissions { get; set; }
        public void Configure(EntityTypeBuilder<PermissionGroup> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("permission_group");
        }


        public IEnumerable<PermissionGroup> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new List<PermissionGroup>
            {
                new PermissionGroup
                {
                    Id = 1,
                    Name = "用户管理",
                },
                new PermissionGroup
                {
                    Id = 2,
                    Name = "权限管理",
                },
                new PermissionGroup
                {
                    Id = 3,
                    Name = "角色管理",
                },
            };
        }
    }
}