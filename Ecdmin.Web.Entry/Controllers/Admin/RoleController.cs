using System.Linq;
using System.Threading.Tasks;
using Ecdmin.Application;
using Ecdmin.Application.Admin.Dtos;
using Ecdmin.Application.Admin.IServices;
using Ecdmin.Application.Admin.Vos;
using Ecdmin.Application.Common.Vos;
using Ecdmin.Core.Entities.Admin;
using Furion.DynamicApiController;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Ecdmin.Web.Entry.Controllers.Admin
{
    public class RoleController : IDynamicApiController
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Get([FromQuery] RoleRequest.Get query)
        {
            var list = await _service.Get(query);
            query.Total = list.TotalCount;
            return Response.Pagination(list.Items.Select(t => t.Adapt<RoleDto.List>()), query);
        }

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

        public async Task<IActionResult> Update(int id, RoleRequest.UpdateInput input)
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