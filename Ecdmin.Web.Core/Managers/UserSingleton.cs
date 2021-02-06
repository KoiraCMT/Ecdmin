using System.Collections.Generic;
using System.Linq;
using Ecdmin.Application.Admin.IServices;
using Ecdmin.Core.Entities.Admin;

namespace Ecdmin.Web.Core.Managers
{
    public sealed class UserSingleton
    {
        private Administrator _user = null;
        private List<string> _permissions = null;
        private UserSingleton() { }

        public Administrator GetUser(int userId, IAdministratorService administratorService)
        {
            return _user ??= administratorService.FindWithRoleIds(userId).Result;
        }

        public Administrator GetUser()
        {
            return _user;
        }

        public IEnumerable<string> GetPermissions()
        {
            return _permissions;
        }

        public IEnumerable<string> GetPermissions(IRoleService roleService)
        {
            if (_permissions != null)
            {
                return _permissions;
            }

            var roleIds = _user.AdministratorRoles.Select(t => t.RoleId).ToList();

            return _permissions = roleService.GetPermissionsByRoleIds(roleIds);
        }

        private static readonly UserSingleton _userInstance = new UserSingleton();

        public static UserSingleton GetInstance => _userInstance;
    }
}