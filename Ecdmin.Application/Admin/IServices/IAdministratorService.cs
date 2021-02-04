using System.Collections.Generic;
using System.Threading.Tasks;
using Ecdmin.Application.Admin.Vos;
using Ecdmin.Core.Entities.Admin;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ecdmin.Application.Admin.IServices
{
    public interface IAdministratorService
    {
        Task<EntityEntry<Administrator>> Add(Administrator adminUser);

        Task<bool> IsExisted(string username);
        
        Task<string> Login(string username, string password);

        Task<Administrator> Find(int id);

        // Task<> Get(PageRequest pageRequest);
        Task<PagedList<Administrator>> GetList(AdministratorRequest.Get getParams);
        
        Task<EntityEntry<Administrator>> Update(int id, AdministratorRequest.EditInput editInput);
        Task Delete(int id);
        Task AssignRole(int id, AdministratorRequest.AssignRole input);
    }
}