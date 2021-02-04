namespace Ecdmin.Core
{
    public static class PermissionConst
    {
        // administrator permission
        public static class Administrator
        {
            public const string INDEX = "administrator.index";
            public const string ADD = "administrator.add";
            public const string UPDATE = "administrator.update";
            public const string DELETE = "administrator.delete";
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