using System.Linq;
using System.Threading.Tasks;
using Ecdmin.Application;
using Ecdmin.Application.Admin.Dtos;
using Ecdmin.Application.Admin.IServices;
using Ecdmin.Application.Admin.Vos;
using Ecdmin.Core.Entities.Admin;
using Furion.DynamicApiController;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecdmin.Web.Entry.Controllers.Admin
{
    [Authorize, ApiDescriptionSettings("管理员管理")]
    public class AdminUserController : IDynamicApiController
    {
        private readonly IAdminUserService _adminUserService;

        public AdminUserController(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }
        
        public async Task<IActionResult> GetList([FromQuery] AdminUserRequest.Get getParams)
        {
            var list = await _adminUserService.GetList(getParams);
            getParams.Total = list.TotalCount;

            return Response.Pagination(list.Items.Select(t => t.Adapt<AdminUserDto.List>()), getParams);
        }

        [SecurityDefine("admin-user.add")]
        public async Task<IActionResult> Add(AdminUserRequest.AddInput addInput)
        {
            var adminUser = addInput.Adapt<AdminUser>();
            if (await _adminUserService.IsExisted(adminUser.Username))
            {
                return Response.BadRequest("username is used.");
            }

            await _adminUserService.Add(adminUser);
            return Response.Success();
        }

        public async Task<IActionResult> Update(int id, AdminUserRequest.EditInput editInput)
        {
            await _adminUserService.Update(id, editInput);
            return Response.Success();
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _adminUserService.Delete(id);
            return Response.Success();
        }
    }
}