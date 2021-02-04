using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Furion.DatabaseAccessor;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecdmin.Core.Entities.Admin
{
    public class Administrator : Entity, IEntityTypeBuilder<Administrator>, IEntitySeedData<Administrator>
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
        [System.Text.Json.Serialization.JsonIgnore]
        public string Password { get; set; }
        
        public string Avatar { get; set; }

        public virtual ICollection<AdministratorRole> AdministratorRoles { get; set; }

        public void Configure(EntityTypeBuilder<Administrator> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.ToTable("administrator");
            
            entityBuilder.HasIndex(u => u.Username);
            //软删除过滤
            entityBuilder.HasQueryFilter(t => !t.IsDeleted);
        }

        public IEnumerable<Administrator> HasData(DbContext dbContext, Type dbContextLocator)
        {
            var administrator = new Administrator { Id = 1, Username = "echo", Password = "123123", Name = "echo", CreatedTime = DateTimeOffset.Now };
            var passwordHasher = new PasswordHasher<Administrator>();
            administrator.Password = passwordHasher.HashPassword(administrator, administrator.Password);
            return new List<Administrator>
            {
                administrator
            };
        }
    }
}