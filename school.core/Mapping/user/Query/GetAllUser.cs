using school.core.features.user.Query.Result;
using school.Data.Entites.identity;

namespace school.core.Mapping.user
{
    public partial class userprofile
    {
        public void GetAllUserMaping()
        {
            CreateMap<User, GetUserListResponse>().ForMember(x => x.FullName, o => o.MapFrom(x => x.FullName))
                  .ForMember(x => x.Email, o => o.MapFrom(x => x.Email))
                  .ForMember(x => x.Address, o => o.MapFrom(x => x.Address))
                  .ForMember(x => x.Countery, o => o.MapFrom(x => x.Countery));





        }


    }
}
