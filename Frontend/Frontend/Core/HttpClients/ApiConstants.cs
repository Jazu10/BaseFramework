namespace Frontend.Core.HttpClients
{
    public static class ApiConstants
    {
        public const string Login = "api/Account/Login/";
        public const string Register = "api/Account/Register/";

        public const string GetAllUsers = "api/Account/UserList/";
        public const string SingleUser = "api/Account/SingleUser/";
        public const string ChangePassword = "api/Account/ChangePassword/";

        public const string GetAllRoles = "api/Administrative/Roles/";
        public const string SingleRole = "api/Administrative/SingleRole/";


        public const string UserRoles = "api/Administrative/UserRoles/";
        public const string UserClaims = "api/Administrative/UserClaims/";
        public const string RoleClaims = "api/Administrative/RoleClaims/";
    }
}
