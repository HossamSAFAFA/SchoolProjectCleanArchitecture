using MediatR;
using school.core.Base;

namespace school.core.features.Authorization.commends.models
{
    public class DeleteRoleCommend : IRequest<Response<string>>
    {

        public int Id { get; set; }
        public DeleteRoleCommend(int id)
        {
            Id = id;
        }
    }
}
