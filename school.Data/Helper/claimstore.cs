using System.Security.Claims;

namespace school.Data.Helper
{
    public static class ClaimStore
    {
        public static List<Claim> cliams = new()
        {
            new Claim ("Create Student","false"),
            new Claim ("Edit Student","false"),
            new Claim ("Update Student","false"),
            new Claim ("Delete Student","false"),
        };
    }
}
