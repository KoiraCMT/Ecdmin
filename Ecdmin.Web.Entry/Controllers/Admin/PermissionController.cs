using System.Threading.Tasks;
using Ecdmin.Application;
using Ecdmin.Application.Admin.IServices;
using Ecdmin.Application.Admin.Vos;
using Ecdmin.Application.Common.Vos;
using Ecdmin.Core.Entities.Admin;
using Furion.DynamicApiController;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecdmin.Web.Entry.Controllers.Admin
{
    [Authorize, ApiDescriptionSettings("权限管理")]
    public class PermissionController : IDynamicApiController
    {
        private readonly IPermissionService _service;

        public PermissionController(IPermissionService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Get([FromQuery] PermissionRequest.Get query)
        {
            var list = await _service.Get(query);
            query.Total = list.TotalCount;
            return Response.Pagination(list.Items, query);
        }


        public async Task<IActionResult> Add([FromBody] PermissionRequest.AddInput input)
        {
            var permission = input.Adapt<Permission>();
            await _service.Add(permission);
            return Response.Success();
        }

        public async Task<IActionResult> Update(int id, PermissionRequest.UpdateInput input)
        {
            await _service.Update(id, input);
            return Response.Success();
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Response.Success();
        }
    }
}