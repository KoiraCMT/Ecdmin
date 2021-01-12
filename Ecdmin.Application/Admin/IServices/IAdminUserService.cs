using System.Collections.Generic;
using System.Threading.Tasks;
using Ecdmin.Application.Admin.Vos;
using Ecdmin.Core.Entities.Admin;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ecdmin.Application.Admin.IServices
{
    public interface IAdminUserService
    {
        Task<EntityEntry<AdminUser>> Add(AdminUser adminUser);

        Task<bool> IsExisted(string username);
        
        Task<string> Login(string username, string password);

        Task<AdminUser> Find(int id);

        // Task<> Get(PageRequest pageRequest);
        Task<PagedList<AdminUser>> GetList(AdminUserRequest.Get getParams);
        
        Task<EntityEntry<AdminUser>> Update(int id, AdminUserRequest.EditInput editInput);
        Task Delete(int id);
    }
}