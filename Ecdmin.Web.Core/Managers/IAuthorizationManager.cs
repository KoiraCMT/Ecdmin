using System.Collections.Generic;
using Ecdmin.Core.Entities.Admin;

namespace Ecdmin.Web.Core.Managers
{
    public interface IAuthorizationManager
    {
        int GetUserId();

        bool CheckPermission(string resourceId);

        Administrator GetUser();

        IEnumerable<string> GetPermissions();
    }
}