namespace school.Data.AppMetaData
{
    public static class Router
    {
        public const string SingleRouting = "/{id}";
        public const string root = "Api";
        public const string Version = "V1";
        public const string rule = root + "/" + Version + "/";
        public static class studentRouting
        {

            public const string prefix = rule + "student";
            public const string List = prefix + "/List";
            public const string GetById = prefix + SingleRouting;
            public const string AddStudent = prefix + "/Created";
            public const string EditStudent = prefix + "/Edit";
            public const string DeleteStudent = prefix + "/Delete";

        }
        public static class UserRouting
        {

            public const string prefix = rule + "User";
            public const string AddUser = prefix + "Add";
            public const string GetAll = rule + "GetAll";
            public const string GetById = rule + "{id}";
            public const string Update = rule + "Update";
            public const string Delete = rule + "Delete" + "{id}";
            public const string changepassord = rule + "changepassord";






        }
        public static class Authentication
        {

            public const string prefix = rule + "Authentication";
            public const string create = prefix + "create";
            public const string RefrashToken = prefix + "Refrash";
            public const string ValidateToken = prefix + "validate";


        }
        public static class Authorize
        {

            public const string prefix = rule + "Authorize";
            public const string create = prefix + "Role/create";
            public const string Edit = prefix + "Role/Edit";
            public const string Delete = prefix + "Role/Delete";
            public const string GetList = prefix + "Role/Get_List_Role";
            public const string GetRole = prefix + "Role/GetRolebyid/{id}";
            public const string GetRoleManageUserRole = prefix + "Role/Manage_User_Role/{id}";
            public const string UpdateUserRole = prefix + "Role/Update_User_Role";
            public const string GetRoleManageClaimsRole = prefix + "Cliams/Get_ClaimsRole/{id}";
            public const string UpdateCliamsRole = "Cliams/Update_User_Cliams";
        }

    }
}
