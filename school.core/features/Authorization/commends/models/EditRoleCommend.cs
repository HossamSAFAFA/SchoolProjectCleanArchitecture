using MediatR;
using school.core.Base;
using school.Data.Dto;

namespace school.core.features.Authorization.commends.models
{
    public class EditRolecommend : EditRole, IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}
