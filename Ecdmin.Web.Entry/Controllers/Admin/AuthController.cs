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
        private readonly IAdministratorService _administratorService;
        private readonly IAuthorizationManager _authorizationManager;

        public AuthController(IAdministratorService administratorService, IAuthorizationManager authorizationManager)
        {
            _administratorService = administratorService;
            _authorizationManager = authorizationManager;
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(AuthRequest.LoginInput input)
        {
            var res = await _administratorService.Login(input.Username, input.Password);

            return Response.Data(new
            {
                token = res
            });
        }

        [HttpGet]
        public async Task<IActionResult> Info()
        {
            var id = _authorizationManager.GetUserId();
            var adminUser = await _administratorService.Find(id);
            return Response.Data(adminUser.Adapt<AuthInfoDto>());
        }

        [HttpPost]
        public IActionResult Logout()
        {
            return Response.Success();
        }
    }
}