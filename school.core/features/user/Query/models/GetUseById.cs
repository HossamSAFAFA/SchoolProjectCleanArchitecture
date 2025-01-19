using MediatR;
using school.core.Base;
using school.core.features.user.Query.Result;

namespace school.core.features.user.Query.models
{
    public class GetUseById : IRequest<Response<GetUserByid>>
    {
        public int id { get; set; }
        public GetUseById(int Id)
        {
            id = Id;
        }

    }
}
