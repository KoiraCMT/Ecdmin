using System.Collections.Generic;
using System.Threading.Tasks;
using Ecdmin.Application.Admin.Vos;
using Ecdmin.Core.Entities.Admin;

namespace Ecdmin.Application.Admin.IServices
{
    public interface IPermissionService
    {
        Task<PagedList<Permission>> Get(PermissionRequest.Get query);

        Task Add(Permission entity);
        Task Update(int id, PermissionRequest.UpdateInput input);
        
        Task Delete(int id);
    }
}