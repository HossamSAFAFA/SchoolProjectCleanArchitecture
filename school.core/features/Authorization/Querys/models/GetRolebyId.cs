using MediatR;
using school.core.Base;
using school.core.features.Authorization.Querys.Result;

namespace school.core.features.Authorization.Querys.models
{
    public class GetRolebyId : IRequest<Response<GetRoleResult>>
    {
        public int Id { get; set; }
        public GetRolebyId(int id)
        {
            Id = id;
        }
    }
}
