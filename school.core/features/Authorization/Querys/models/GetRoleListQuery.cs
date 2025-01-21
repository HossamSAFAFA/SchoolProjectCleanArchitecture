using MediatR;
using school.core.Base;
using school.core.features.Authorization.Querys.Result;

namespace school.core.features.Authorization.Querys.models
{
    public class GetRoleListQuery : IRequest<Response<List<GetRoleResult>>>
    {
    }
}
