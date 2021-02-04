using System.Linq;
using System.Threading.Tasks;
using Ecdmin.Application;
using Ecdmin.Application.Admin.Dtos;
using Ecdmin.Application.Admin.IServices;
using Ecdmin.Application.Admin.Vos;
using Ecdmin.Core;
using Ecdmin.Core.Entities.Admin;
using Furion.DynamicApiController;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecdmin.Web.Entry.Controllers.Admin
{
    [ApiDescriptionSettings("角色")]
    public class RoleController : IDynamicApiController
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [SecurityDefine(PermissionConst.Role.INDEX)]
        public async Task<IActionResult> Get([FromQuery] RoleRequest.Get query)
        {
            var list = await _service.Get(query);
            query.Total = list.TotalCount;
            return Response.Pagination(list.Items.Select(t => t.Adapt<RoleDto.List>()), query);
        }

        [SecurityDefine(PermissionConst.Role.ADD)]
        public async Task<IActionResult> Add(RoleRequest.AddInput input)
        {
            if (await _service.IsExisted(input.Name))
            {
                return Response.BadRequest("role is used.");
            }
            var role = input.Adapt<Role>();
            await _service.Add(role);
            return Response.Success();
        }

        [SecurityDefine(PermissionConst.Role.UPDATE)]
        public async Task<IActionResult> Update(int id, RoleRequest.UpdateInput input)
        {
            await _service.Update(id, input);
            return Response.Success();
        }

        [SecurityDefine(PermissionConst.Role.DELETE)]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Response.Success(); 
        }

        [SecurityDefine(PermissionConst.Role.ASSIGN_PERMISSION)]
        public async Task<IActionResult> FindWithPermissions(int id)
        {
            var role = await _service.FindWithPermissions(id);
            return Response.Data(role.Adapt<RoleDto.WithPermission>());
        }

        [SecurityDefine(PermissionConst.Role.ASSIGN_PERMISSION)]
        [HttpPost]
        public async Task<IActionResult> AssignPermission(int id, RoleRequest.AssignPermissionInput input)
        {
            await _service.AssignPermission(id, input);
            return Response.Success();
        }
    }
}