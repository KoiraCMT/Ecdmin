using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Furion.DatabaseAccessor;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecdmin.Core.Entities.Admin
{
    public class AdminUser : Entity, IEntityTypeBuilder<AdminUser>, IEntitySeedData<AdminUser>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(30), Required]
        public string Username { get; set; }
        
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(50), Required]
        public string Name { get; set; }
        
        [MaxLength(100), Required]
        [JsonIgnore] 
        public string Password { get; set; }
        
        public string Avatar { get; set; }

        public void Configure(EntityTypeBuilder<AdminUser> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("admin_user");
            
            entityBuilder.HasIndex(u => u.Username);
            //软删除过滤
            entityBuilder.HasQueryFilter(t => !t.IsDeleted);
        }

        public IEnumerable<AdminUser> HasData(DbContext dbContext, Type dbContextLocator)
        {
            var adminUser = new AdminUser { Id = 1, Username = "echo", Password = "123123", Name = "echo", CreatedTime = DateTimeOffset.Now };
            var passwordHasher = new PasswordHasher<AdminUser>(); 
            adminUser.Password = passwordHasher.HashPassword(adminUser, adminUser.Password);
            return new List<AdminUser>
            {
               adminUser
            };
        }
    }
}