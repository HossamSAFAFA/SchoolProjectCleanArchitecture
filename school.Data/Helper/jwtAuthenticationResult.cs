namespace school.Data.Helper
{
    public class jwtAuthenticationResult
    {
        public string Accesstoken { get; set; }
        public Refrashtoken refrashtoken { get; set; }




    }
    public class Refrashtoken
    {
        public string userName { get; set; }
        public string TokeString { get; set; }
        public DateTime ExpiredAt { get; set; }

    }
}
