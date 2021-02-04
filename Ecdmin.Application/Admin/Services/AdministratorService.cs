using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecdmin.Application.Admin.IServices;
using Ecdmin.Application.Admin.Vos;
using Ecdmin.Application.Common;
using Ecdmin.Application.Utils;
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
    public class AdministratorService : IAdministratorService, ITransient 
    {
        private readonly IRepository<Administrator> _administratorRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdministratorService(IRepository<Administrator> administratorRepository, IHttpContextAccessor httpContextAccessor)
        {
            _administratorRepository = administratorRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<EntityEntry<Administrator>> Add(Administrator administrator)
        {
            administrator.Password = PasswordUtil.HashPassword(administrator, administrator.Password);
            administrator.CreatedTime = DateTimeOffset.Now;
            return await _administratorRepository.InsertAsync(administrator);
        }

        public async Task<bool> IsExisted(string username)
        {
            return await _administratorRepository.AnyAsync(t => t.Username == username);
        }

        public async Task<string> Login(string username, string password)
        {
            var administrator = await _administratorRepository.Where(t => t.Username == username).FirstOrDefaultAsync();

            if (administrator == null || !PasswordUtil.VerifyHashedPassword(administrator, administrator.Password, password))
                throw Oops.Oh("用户名或密码错误").StatusCode(400);
            
            var token = JwtUtil.Generate(administrator.Id.ToString(), administrator.Username);
            _httpContextAccessor.SigninToSwagger(token);
            return token;
        }

        public async Task<Administrator> Find(int id)
        {
            return await _administratorRepository.FindOrDefaultAsync(id);
        }

        public async Task<Administrator> FindWithRoleIds(int id)
        {
            return await _administratorRepository.Include(t => t.AdministratorRoles).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<PagedList<Administrator>> GetList(AdministratorRequest.Get getParams)
        {
            var query = _administratorRepository.AsQueryable()
                .Include(t => t.AdministratorRoles)
                .ThenInclude(t => t.Role).AsQueryable();
            if (!getParams.Name.IsNullOrEmpty())
            {
                query = _administratorRepository.Where(t => t.Name.Contains(getParams.Name));
            }

            return await query.OrderByDescending(t => t.Id)
                .ToPagedListAsync(getParams.Page, getParams.PageSize);
        }

        public async Task<EntityEntry<Administrator>> Update(int id, AdministratorRequest.EditInput editInput)
        {
            var administrator = await _administratorRepository.FindOrDefaultAsync(id);
            if (administrator == null)
            {
                ExceptionService.NotFound();
            }

            if (editInput.Password != null)
            {
                administrator.Password = PasswordUtil.HashPassword(administrator, editInput.Password);
            }

            administrator.Name = editInput.Name;
            administrator.UpdatedTime = DateTimeOffset.Now;
            administrator.Avatar = editInput.Avatar;

            return await administrator.UpdateAsync();
        }

        public async Task Delete(int id)
        {
            await _administratorRepository.FakeDeleteAsync(id);
        }

        public async Task AssignRole(int id, AdministratorRequest.AssignRole input)
        {
            var administrator = await _administratorRepository.Include(t => t.AdministratorRoles).FirstOrDefaultAsync(t => t.Id == id);
            if (administrator == null) ExceptionService.NotFound();
            administrator.AdministratorRoles.Where(e =>
                !input.RoleIds.Contains(e.RoleId)).ToList().ForEach(t => t.Delete());
            input.RoleIds.Where(e => !administrator.AdministratorRoles.Select(t => t.RoleId).Contains(e))
                .ToList()
                .ForEach(t => administrator.AdministratorRoles.Add(new AdministratorRole
                {
                    AdministratorId = id,
                    RoleId = t
                }));
            await administrator.UpdateAsync();
        }
    }
}