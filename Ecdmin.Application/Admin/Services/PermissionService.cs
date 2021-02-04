using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecdmin.Application.Admin.IServices;
using Ecdmin.Application.Admin.Vos;
using Ecdmin.Core.Entities.Admin;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;

namespace Ecdmin.Application.Admin.Services
{
    public class PermissionService : IPermissionService, ITransient
    {
        private readonly IRepository<Permission> _repository;

        public PermissionService(IRepository<Permission> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Permission>> GetByPermissionGroupId(int permissionGroupId)
        {
            return await _repository.AsAsyncEnumerable(t => t.PermissionGroupId == permissionGroupId);
        }

        public Task<PagedList<Permission>> Get(PermissionRequest.Get query)
        {
            return _repository.AsQueryable()
                .Where(query.PermissionGroupId != null, 
                    t => t.PermissionGroupId == query.PermissionGroupId)
                .OrderBy(t => t.Id).ToPagedListAsync(query.Page, query.PageSize);
        }

        public async Task Add(Permission entity)
        {
            await _repository.InsertAsync(entity);
        }

        public async Task Update(int id, PermissionRequest.UpdateInput input)
        {
            var permission = await _repository.FindAsync(id);
            permission.Name = input.Name;
            permission.DisplayName = input.DisplayName;
            await _repository.UpdateAsync(permission);
        }
        
        public async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}