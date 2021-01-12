using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecdmin.Application.Admin.IServices;
using Ecdmin.Application.Common.Vos;
using Ecdmin.Core.Entities.Admin;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecdmin.Application.Admin.Services
{
    public class PermissionGroupService : IPermissionGroupService, ITransient 
    {
        private readonly IRepository<PermissionGroup> _repository;

        public PermissionGroupService(IRepository<PermissionGroup> repository)
        {
            _repository = repository;
        }

        public async Task Add(PermissionGroup permissionGroup)
        { 
            await _repository.InsertAsync(permissionGroup);
        }

        public async Task<PagedList<PermissionGroup>> Get(PageRequest query)
        {
            return await _repository.AsQueryable()
                .OrderBy(t => t.Id)
                .ToPagedListAsync(query.Page, query.PageSize);
        }

        public async Task Update(int id, PermissionGroup input)
        {
            var permissionGroup = await _repository.FindAsync(id);
            permissionGroup.Name = input.Name;
            await _repository.UpdateAsync(permissionGroup);
        }

        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<PermissionGroup>> GetGroupWithPermissions()
        {
            return await _repository.Include(t => t.Permissions).ToListAsync();
        }
    }
}