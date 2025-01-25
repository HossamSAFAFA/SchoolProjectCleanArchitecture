namespace school.Data.result
{
    public class ManageUserResult
    {

        public int UserId { get; set; }
        public List<Role> Roles { get; set; }

    }
    public class Role
    {

        public int id { get; set; }
        public string name { get; set; }
        public bool HasRole { get; set; } = false;
    }
}
