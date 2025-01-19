using MediatR;
using school.core.Base;

namespace school.core.features.user.commend.models
{
    public class ChangePasswordCommend : IRequest<Response<string>>
    {
        public int id { get; set; }
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
        public string confirmPassword { get; set; }
        public ChangePasswordCommend(int Id)
        {
            id = Id;
        }
    }
}
