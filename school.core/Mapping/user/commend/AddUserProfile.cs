using school.core.features.user.commend.models;
using school.Data.Entites.identity;

namespace school.core.Mapping.user
{
    public partial class userprofile
    {

        public void AdduserMaping()
        {
            CreateMap<AddUserCommend, User>().ForMember(x => x.FullName, o => o.MapFrom(x => x.FullName))
                  .ForMember(x => x.Email, o => o.MapFrom(x => x.Email))
                  .ForMember(x => x.UserName, o => o.MapFrom(x => x.UserName))
                  .ForMember(x => x.Address, o => o.MapFrom(x => x.Address))
                  .ForMember(x => x.Countery, o => o.MapFrom(x => x.country))
                  .ForMember(x => x.PhoneNumber, o => o.MapFrom(x => x.phoneNumber));



        }







    }

}
