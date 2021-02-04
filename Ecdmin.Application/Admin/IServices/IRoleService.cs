using System.Collections.Generic;
using System.Threading.Tasks;
using Ecdmin.Application.Admin.Vos;
using Ecdmin.Core.Entities.Admin;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ecdmin.Application.Admin.IServices
{
    public interface IRoleService
    {
        Task<PagedList<Role>> Get(RoleRequest.Get query);

        Task<EntityEntry<Role>> Add(Role role);

        Task<bool> IsExisted(string name);

        Task<EntityEntry<Role>> Update(int id, RoleRequest.UpdateInput input);

        Task Delete(int id);

        Task<Role> FindWithPermissions(int id);

        Task AssignPermission(int id, RoleRequest.AssignPermissionInput input);

        Task<List<Role>> All();
    }
}