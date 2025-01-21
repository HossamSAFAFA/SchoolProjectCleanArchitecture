using MediatR;
using school.core.Base;

namespace school.core.features.Authorization.commends.models
{
    public class AddRoleCommend : IRequest<Response<string>>
    {

        public string RoleName { get; set; }
    }
}
