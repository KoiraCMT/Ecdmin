using System.Security.Claims;
using Ecdmin.Application.Utils;
using Ecdmin.Core.Entities.Admin;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Ecdmin.Web.Core.Managers
{
    public class AuthorizationManager : IAuthorizationManager, ITransient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Administrator> _administratorRepository;
        private readonly ILogger<AuthorizationManager> _logger;

        public AuthorizationManager(IHttpContextAccessor httpContextAccessor
            , IRepository<Administrator> administratorRepository, ILogger<AuthorizationManager> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _administratorRepository = administratorRepository;
            _logger = logger;
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