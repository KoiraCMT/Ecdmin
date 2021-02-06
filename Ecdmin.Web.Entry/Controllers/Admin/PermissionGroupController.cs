using System.Linq;
using System.Threading.Tasks;
using Ecdmin.Application;
using Ecdmin.Application.Admin.IServices;
using Ecdmin.Application.Common.Vos;
using Ecdmin.Core;
using Ecdmin.Core.Entities.Admin;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Profiling.Internal;

namespace Ecdmin.Web.Entry.Controllers.Admin
{
    [ApiDescriptionSettings("权限组管理")]
    public class PermissionGroupController : IDynamicApiController
    {
        private readonly IPermissionGroupService _service;

        public PermissionGroupController(IPermissionGroupService service)
        {
            _service = service;
        }

        [SecurityDefine(PermissionConst.Permission.ADD)]
        public async Task<IActionResult> Add(PermissionGroup permissionGroup)
        {
            await _service.Add(permissionGroup);
            return Response.Success();
        }

        [SecurityDefine(PermissionConst.Permission.INDEX)]
        public async Task<IActionResult> Get([FromQuery] PageRequest query)
        {
            var list = await _service.Get(query);
            query.Total = list.TotalCount;
            return Response.Pagination(list.Items, query);
        }

        [SecurityDefine(PermissionConst.Permission.UPDATE)]
        public async Task<IActionResult> Update(int id, PermissionGroup permissionGroup)
        {
            await _service.Update(id, permissionGroup);
            return Response.Success();
        }

        [SecurityDefine(PermissionConst.Permission.DELETE)]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Response.Success();
        }

        [SecurityDefine(PermissionConst.Role.ASSIGN_PERMISSION)]
        public async Task<IActionResult> GetWithPermissions()
        {
            var groupWithPermissions = await _service.GetGroupWithPermissions();
            return Response.Data(groupWithPermissions);
        }
    }
}