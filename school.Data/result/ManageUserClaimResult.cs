namespace school.Data.result
{
    public class ManageUserClaimResult
    {
        public int UserId { get; set; }
        public List<UserClaim> userclaims { get; set; }

    }
    public class UserClaim
    {


        public string name { get; set; }
        public bool value { get; set; } = false;
    }


}

