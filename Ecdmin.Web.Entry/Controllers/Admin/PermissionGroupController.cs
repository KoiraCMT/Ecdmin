using System.Linq;
using System.Threading.Tasks;
using Ecdmin.Application;
using Ecdmin.Application.Admin.IServices;
using Ecdmin.Application.Common.Vos;
using Ecdmin.Core.Entities.Admin;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Profiling.Internal;

namespace Ecdmin.Web.Entry.Controllers.Admin
{
    [Authorize, ApiDescriptionSettings("权限组管理")]
    public class PermissionGroupController : IDynamicApiController
    {
        private readonly IPermissionGroupService _service;

        public PermissionGroupController(IPermissionGroupService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Add(PermissionGroup permissionGroup)
        {
            await _service.Add(permissionGroup);
            return Response.Success();
        }

        public async Task<IActionResult> Get([FromQuery] PageRequest query)
        {
            var list = await _service.Get(query);
            query.Total = list.TotalCount;
            return Response.Pagination(list.Items.Select(t => new
            {
                t.Id,
                t.Name
            }), query);
        }

        public async Task<IActionResult> Update(int id, PermissionGroup permissionGroup)
        {
            await _service.Update(id, permissionGroup);
            return Response.Success();
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Response.Success();
        }

        public async Task<IActionResult> GetWithPermissions()
        {
            var groupWithPermissions = await _service.GetGroupWithPermissions();
            return Response.Data(groupWithPermissions);
        }
    }
}