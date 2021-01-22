using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecdmin.Application.Admin.IServices;
using Ecdmin.Application.Admin.Vos;
using Ecdmin.Application.Common;
using Ecdmin.Core.Entities.Admin;
using EFCore.BulkExtensions;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.LinqBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ecdmin.Application.Admin.Services
{
    public class RoleService : IRoleService,ITransient 
    {
        private readonly IRepository<Role> _repository;
        private readonly IRepository<RolePermission> _rolePermissionRepository;

        public RoleService(IRepository<Role> repository, IRepository<RolePermission> rolePermissionRepository)
        {
            _repository = repository;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<PagedList<Role>> Get(RoleRequest.Get query)
        {
            return await _repository.AsQueryable()
                .Where(!query.DisplayName.IsNullOrEmpty(), t => t.DisplayName.Contains(query.DisplayName))
                .OrderBy(t => t.Id)
                .ToPagedListAsync(query.Page, query.PageSize);
        }

        public async Task<EntityEntry<Role>> Add(Role role)
        {
            role.CreatedTime = DateTimeOffset.Now;
            return await _repository.InsertAsync(role);
        }

        public async Task<bool> IsExisted(string name)
        {
            return await _repository.AnyAsync(t => t.Name == name);
        }

        public async Task<EntityEntry<Role>> Update(int id, RoleRequest.UpdateInput input)
        {
            var role = await _repository.FindOrDefaultAsync(id);
            if (role == null)
            {
                ExceptionService.NotFound();
            }
            role.Name = input.Name;
            role.DisplayName = input.DisplayName;
            role.Description = input.Description;
            role.UpdatedTime = DateTimeOffset.Now;
            return await _repository.UpdateAsync(role);
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
            await _rolePermissionRepository.Where(t => t.RoleId == id).BatchDeleteAsync();
        }

        public async Task<Role> FindWithPermissions(int id)
        {
            var role = await _repository.Include(t => t.RolePermissions).ThenInclude(t => t.Permission).FirstOrDefaultAsync(t => t.Id == id);
            if (role == null) ExceptionService.NotFound();
            return role;
        }

        public async Task AssignPermission(int id, RoleRequest.AssignPermissionInput input)
        {
            var role = await _repository.Include(t => t.RolePermissions).FirstOrDefaultAsync(t => t.Id == id);
            if (role == null) ExceptionService.NotFound();

            role.RolePermissions.Where(e =>
                !input.PermissionIds.Contains(e.PermissionId)).ToList().ForEach(t => t.Delete());

            input.PermissionIds.Where(e => !role.RolePermissions.Select(t => t.PermissionId).Contains(e))
                .ToList()
                .ForEach(t => role.RolePermissions.Add(new RolePermission
                {
                    PermissionId = t,
                    RoleId = id
                })); 
            await role.UpdateAsync();
        }
    }
}