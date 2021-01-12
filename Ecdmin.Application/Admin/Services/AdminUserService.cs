using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecdmin.Application.Admin.IServices;
using Ecdmin.Application.Admin.Vos;
using Ecdmin.Application.Utils;
using Ecdmin.Core;
using Ecdmin.Core.Entities.Admin;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Furion.LinqBuilder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ecdmin.Application.Admin.Services
{
    public class AdminUserService : IAdminUserService, ITransient 
    {
        private readonly IRepository<AdminUser> _adminUserRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminUserService(IRepository<AdminUser> adminUserRepository, IHttpContextAccessor httpContextAccessor)
        {
            _adminUserRepository = adminUserRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<EntityEntry<AdminUser>> Add(AdminUser adminUser)
        {
            adminUser.Password = PasswordUtil.HashPassword(adminUser, adminUser.Password);
            adminUser.CreatedTime = DateTimeOffset.Now;
            return await _adminUserRepository.InsertAsync(adminUser);
        }

        public async Task<bool> IsExisted(string username)
        {
            return await _adminUserRepository.AnyAsync(t => t.Username == username);
        }

        public async Task<string> Login(string username, string password)
        {
            var adminUser = await _adminUserRepository.Where(t => t.Username == username).FirstOrDefaultAsync();

            if (adminUser == null || !PasswordUtil.VerifyHashedPassword(adminUser, adminUser.Password, password))
                throw Oops.Oh("用户名或密码错误").StatusCode(400);
            
            var token = JwtUtil.Generate(adminUser.Id.ToString(), adminUser.Username);
            _httpContextAccessor.SigninToSwagger(token);
            return token;
        }

        public async Task<AdminUser> Find(int id)
        {
            return await _adminUserRepository.FindOrDefaultAsync(id);
        }

        public async Task<PagedList<AdminUser>> GetList(AdminUserRequest.Get getParams)
        {
            var query = _adminUserRepository.AsQueryable();
            if (!getParams.Name.IsNullOrEmpty())
            {
                query = _adminUserRepository.Where(t => t.Name.Contains(getParams.Name));
            }

            return await query.OrderByDescending(t => t.Id)
                .ToPagedListAsync(getParams.Page, getParams.PageSize);
        }

        public async Task<EntityEntry<AdminUser>> Update(int id, AdminUserRequest.EditInput editInput)
        {
            var adminUser = await _adminUserRepository.FindOrDefaultAsync(id);
            if (adminUser == null)
            {
                throw Oops.Oh(ErrorCode.NOT_FOUND, id).StatusCode(404);
            }

            if (editInput.Password != null)
            {
                adminUser.Password = PasswordUtil.HashPassword(adminUser, editInput.Password);
            }

            adminUser.Name = editInput.Name;
            adminUser.UpdatedTime = DateTimeOffset.Now;
            adminUser.Avatar = editInput.Avatar;
            
            return await adminUser.UpdateAsync();
        }

        public async Task Delete(int id)
        {
            await _adminUserRepository.FakeDeleteAsync(id);
        }
    }
}