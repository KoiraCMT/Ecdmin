namespace Ecdmin.Core
{
    public static class PermissionConst
    {
        // admin-user permission
        public static class AdminUser
        {
            public const string INDEX = "admin-user.index";
            public const string ADD = "admin-user.add";
            public const string UPDATE = "admin-user.update";
            public const string DELETE = "admin-user.delete";
        }

        // permission node
        public static class Permission
        {
            public const string INDEX = "permission.index";
            public const string ADD = "permission.add";
            public const string UPDATE = "permission.update";
            public const string DELETE = "permission.delete";
        }

        public static class Role
        {
            public const string INDEX = "role.index";
            public const string ADD = "role.add";
            public const string UPDATE = "role.update";
            public const string DELETE = "role.delete";
            public const string ASSIGN_PERMISSION = "role.assignPermission";
        }
    }
}