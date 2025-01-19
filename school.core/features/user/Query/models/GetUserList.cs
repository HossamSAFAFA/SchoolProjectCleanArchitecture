using MediatR;
using school.core.Base;
using school.core.features.user.Query.Result;

namespace school.core.features.user.Query.models
{
    public class GetUserList : IRequest<Response<List<GetUserListResponse>>>
    {


    }
}
