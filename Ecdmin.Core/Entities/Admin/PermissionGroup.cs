using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Furion.DatabaseAccessor;
using Furion.DataValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecdmin.Core.Entities.Admin
{
    public class PermissionGroup : EntityBase, IEntityTypeBuilder<PermissionGroup>, IEntitySeedData<PermissionGroup>
    {
        [Required, MaxLength(50)]
        [DataValidation(ValidationTypes.Ascii)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string DisplayName { get; set; }
        
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
                    Name = "administrator",
                    DisplayName = "用户管理",
                },
                new PermissionGroup
                {
                    Id = 2,
                    Name = "permission",
                    DisplayName = "权限管理",
                },
                new PermissionGroup
                {
                    Id = 3,
                    Name = "role",
                    DisplayName = "角色管理",
                },
            };
        }
    }
}