using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecdmin.Core.Entities.Admin
{
    public class Permission : EntityBase, IEntityTypeBuilder<Permission>, IEntitySeedData<Permission>
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

        public IEnumerable<Permission> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new List<Permission>
            {
                new Permission
                {
                    Id = 1,
                    PermissionGroupId = 1,
                    Name = "administrator.index",
                    DisplayName = "首页"
                },
                new Permission
                {
                    Id = 2,
                    PermissionGroupId = 1,
                    Name = "administrator.add",
                    DisplayName = "新增"
                },
                new Permission
                {
                    Id = 3,
                    PermissionGroupId = 1,
                    Name = "administrator.update",
                    DisplayName = "修改"
                },
                new Permission
                {
                    Id = 4,
                    PermissionGroupId = 1,
                    Name = "administrator.delete",
                    DisplayName = "删除"
                },
                new Permission
                {
                    Id = 5,
                    PermissionGroupId = 2,
                    Name = "permission.index",
                    DisplayName = "首页"
                }
            };
        }
    }
}