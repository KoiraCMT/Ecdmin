using Ecdmin.Web.Core.Managers;
using Furion.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Ecdmin.Web.Core.Handlers
{
    public class JwtHandler : AppAuthorizeHandler
    {
        public override bool Pipeline(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
        {
            // 这里写您的授权判断逻辑，授权通过返回 true，否则返回 false
            return CheckAuthorize(httpContext);
        }
        
        private static bool CheckAuthorize(DefaultHttpContext httpContext)
        {
            // 获取权限特性
            var securityDefineAttribute = httpContext.GetMetadata<SecurityDefineAttribute>();
            if (securityDefineAttribute == null) return true;

            var authorizationManager = httpContext.RequestServices.GetService<IAuthorizationManager>();
            
            return authorizationManager.CheckPermission(securityDefineAttribute.ResourceId);
        }
    }
}