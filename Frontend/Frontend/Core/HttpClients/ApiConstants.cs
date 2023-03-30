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
        public const string UserInRole = "api/Account/GetUsersInRole/";

        public const string GetAllAds = "api/Employee/AdvertisementList/";
        public const string GetAllAdsById = "api/Employee/UsersAdvertisement/";
        public const string DltAdsById = "api/Employee/SingleAdvertisement/";
        public const string EditAdsById = "api/Employee/SingleAdvertisement/";


        public const string GetAllNews = "api/Employee/NewsList/";
        public const string AddNews = "api/Employee/NewsList/";
        public const string GetNewsByUser = "api/Employee/UsersNews/";
        public const string EditNews = "api/Employee/SingleNews/";
        public const string DeleteNews = "api/Administrative/SingleNews/";

        public const string GetAllPosts = "api/Employee/PostList/";
        public const string UserPosts = "api/Employee/UsersPost";
        public const string GetAllPostsAdmin = "api/Employee/AdminPosts/";
        public const string ApprovePost = "api/Employee/ApprovePost/";
        public const string RejectPost = "api/Employee/SinglePost";

        public const string Comment = "api/Employee/Comment/";
        public const string GetAllComments = "api/Employee/Comment";


        public const string AddDltLikes = "api/Employee/Liker";

    }
}
