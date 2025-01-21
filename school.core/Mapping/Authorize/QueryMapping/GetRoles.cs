using AutoMapper;
using Microsoft.AspNetCore.Identity;
using school.core.features.Authorization.Querys.Result;

namespace school.core.Mapping.Authorize
{
    public partial class Autorize : Profile
    {
        public void GetRoleMapping()
        {
            CreateMap<IdentityRole<int>, GetRoleResult>().ForMember(x => x.Id, o => o.MapFrom(x => x.Id))
                .ForMember(x => x.Name, o => o.MapFrom(x => x.Name));
        }
    }
}
