using System.Threading.Tasks;
using Ecdmin.Application;
using Ecdmin.Application.Admin.Dtos;
using Ecdmin.Application.Admin.IServices;
using Ecdmin.Application.Admin.Vos;
using Ecdmin.Web.Core.Managers;
using Furion.DynamicApiController;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecdmin.Web.Entry.Controllers.Admin
{
    public class AuthController : IDynamicApiController 
    {
        private readonly IAdminUserService _adminUserService;
        private readonly IAuthorizationManager _authorizationManager;

        public AuthController(IAdminUserService adminUserService, IAuthorizationManager authorizationManager)
        {
            _adminUserService = adminUserService;
            _authorizationManager = authorizationManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthRequest.LoginInput input)
        {
            var res = await _adminUserService.Login(input.Username, input.Password);

            return Response.Data(new
            {
                token = res
            });
        }

        [HttpGet]
        [AppAuthorize]
        public async Task<IActionResult> Info()
        {
            var id = _authorizationManager.GetUserId();
            var adminUser = await _adminUserService.Find(id);
            return Response.Data(adminUser.Adapt<AuthInfoDto>());
        }

        [HttpPost]
        [AppAuthorize]
        public IActionResult Logout()
        {
            return Response.Success();
        }
    }
}