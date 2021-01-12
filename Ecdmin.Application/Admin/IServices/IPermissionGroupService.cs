using System.Collections.Generic;
using System.Threading.Tasks;
using Ecdmin.Application.Common.Vos;
using Ecdmin.Core.Entities.Admin;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ecdmin.Application.Admin.IServices
{
    public interface IPermissionGroupService
    {
        Task Add(PermissionGroup permissionGroup);

        Task<PagedList<PermissionGroup>> Get(PageRequest query);

        Task Update(int id, PermissionGroup permissionGroup);
        
        Task Delete(int id);
        Task<List<PermissionGroup>> GetGroupWithPermissions();
    }
}