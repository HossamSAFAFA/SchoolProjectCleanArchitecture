using MediatR;
using school.core.Base;
using school.Data.result;

namespace school.core.features.Authorization.Querys.models
{
    public class ManageUserRoleQuery : IRequest<Response<ManageUserResult>>
    {
        public int UserId { get; set; }
        public ManageUserRoleQuery(int id)
        {
            UserId = id;
        }
    }
}
