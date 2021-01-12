using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecdmin.Application.Admin.IServices;
using Ecdmin.Application.Admin.Vos;
using Ecdmin.Application.Common.Vos;
using Ecdmin.Core.Entities.Admin;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.LinqBuilder;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ecdmin.Application.Admin.Services
{
    public class RoleService : IRoleService,ITransient 
    {
        private readonly IRepository<Role> _repository;

        public RoleService(IRepository<Role> repository)
        {
            _repository = repository;
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
            var role = await _repository.FindAsync(id);
            role.Name = input.Name;
            role.DisplayName = input.DisplayName;
            role.Description = input.Description;
            role.UpdatedTime = DateTimeOffset.Now;
            return await _repository.UpdateAsync(role);
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}