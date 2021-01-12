using System.Security.Claims;
using Ecdmin.Application.Utils;
using Ecdmin.Core.Entities.Admin;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace Ecdmin.Web.Core.Managers
{
    public class AuthorizationManager : IAuthorizationManager, ITransient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<AdminUser> _adminUserRepository;

        public AuthorizationManager(IHttpContextAccessor httpContextAccessor
            , IRepository<AdminUser> adminUserRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _adminUserRepository = adminUserRepository;
        }
        
        public int GetUserId()
        {
            return int.Parse(DESUtil.DESDecrypt(_httpContextAccessor.HttpContext.User.FindFirstValue("user_id")));
        }

        public bool CheckPermission(string resourceId)
        {
            var userId = GetUserId();
            return true;
        }
    }
}