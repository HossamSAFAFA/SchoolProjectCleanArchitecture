using MediatR;
using school.core.Base;

namespace school.core.features.user.commend.models
{
    public class DeleteUserCommend : IRequest<Response<string>>
    {
        public int id { get; set; }
        public DeleteUserCommend(int Id)
        {
            id = Id;
        }
    }
}
