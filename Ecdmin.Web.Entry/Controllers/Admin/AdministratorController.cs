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
    [ApiDescriptionSettings("管理员管理")]
    public class AdministratorController : IDynamicApiController
    {
        private readonly IAdministratorService _administratorService;

        public AdministratorController(IAdministratorService administratorService)
        {
            _administratorService = administratorService;
        }

        [SecurityDefine(PermissionConst.AdminUser.INDEX)]
        public async Task<IActionResult> GetList([FromQuery] AdministratorRequest.Get getParams)
        {
            var list = await _administratorService.GetList(getParams);
            getParams.Total = list.TotalCount;

            return Response.Pagination(list.Items.Select(t => t.Adapt<AdministratorDto.List>()), getParams);
        }

        [SecurityDefine(PermissionConst.AdminUser.ADD)]
        public async Task<IActionResult> Add(AdministratorRequest.AddInput addInput)
        {
            var adminUser = addInput.Adapt<Administrator>();
            if (await _administratorService.IsExisted(adminUser.Username))
            {
                return Response.BadRequest("username is used.");
            }

            await _administratorService.Add(adminUser);
            return Response.Success();
        }

        [SecurityDefine(PermissionConst.AdminUser.UPDATE)]
        public async Task<IActionResult> Update(int id, AdministratorRequest.EditInput editInput)
        {
            await _administratorService.Update(id, editInput);
            return Response.Success();
        }

        [SecurityDefine(PermissionConst.AdminUser.DELETE)]
        public async Task<IActionResult> Delete(int id)
        {
            await _administratorService.Delete(id);
            return Response.Success();
        }
    }
}