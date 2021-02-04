using System.Linq;
using System.Security.Claims;
using Ecdmin.Application.Admin.IServices;
using Ecdmin.Application.Utils;
using Ecdmin.Core.Entities.Admin;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using StackExchange.Profiling.Internal;

namespace Ecdmin.Web.Core.Managers
{
    public class AuthorizationManager : IAuthorizationManager, ITransient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAdministratorService _administratorService;
        private readonly IRoleService _roleService;
        private readonly ILogger<AuthorizationManager> _logger;

        public AuthorizationManager(IHttpContextAccessor httpContextAccessor,
            IAdministratorService administratorService,
            IRoleService roleService,
            ILogger<AuthorizationManager> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _administratorService = administratorService;
            _roleService = roleService;
            _logger = logger;
        }
        
        public int GetUserId()
        {
            return int.Parse(DESUtil.DESDecrypt(_httpContextAccessor.HttpContext.User.FindFirstValue("user_id")));
        }

        public bool CheckPermission(string resourceId)
        {
            var userId = GetUserId();

            //超管
            if (userId == 1) return true;

            var administrator = _administratorService.FindWithRoleIds(userId).Result;

            if (administrator == null) return false;

            var roleIds = administrator.AdministratorRoles.Select(t => t.RoleId).ToList();

            var permissions = _roleService.GetPermissionsByRoleIds(roleIds);

            return permissions.Contains(resourceId);
        }
    }
}