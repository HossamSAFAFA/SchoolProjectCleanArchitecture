using MediatR;
using school.core.Base;
using school.Data.result;

namespace school.core.features.Authorization.Querys.models
{
    public class ManageUserClaimQuery : IRequest<Response<ManageUserClaimResult>>
    {
        public int UserId { get; set; }
        public ManageUserClaimQuery(int id)
        {
            UserId = id;
        }
    }
}
