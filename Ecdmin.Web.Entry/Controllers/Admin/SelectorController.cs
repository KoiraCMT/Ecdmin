using System.Linq;
using System.Threading.Tasks;
using Ecdmin.Application;
using Ecdmin.Application.Admin.Dtos;
using Ecdmin.Application.Admin.IServices;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;

namespace Ecdmin.Web.Entry.Controllers.Admin
{
    [ApiDescriptionSettings("选项")]
    public class SelectorController : IDynamicApiController
    {
        [HttpGet]
        public async Task<IActionResult> Roles([FromServices] IRoleService service)
        {
            var roles = await service.All();
            return Response.Data(roles.Select(t => new LabelValueDto<string, int>
            {
                Label = t.DisplayName,
                Value = t.Id,
            }));
        }
    }
}