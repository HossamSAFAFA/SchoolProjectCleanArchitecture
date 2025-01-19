using AutoMapper;

namespace school.core.Mapping.user
{
    public partial class userprofile : Profile
    {
        public userprofile()
        {
            UpdateuserMaping();
            GetAllUserMaping();
            GetUserByIdMaping();
            AdduserMaping();
        }

    }
}
