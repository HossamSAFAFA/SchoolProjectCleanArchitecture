using MediatR;
using school.core.Base;
using school.Data.requesr;

namespace school.core.features.Authorization.commends.models
{
    public class EditRolecommend : EditRole, IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}
