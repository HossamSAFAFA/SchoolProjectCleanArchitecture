using MediatR;
using school.core.Base;

namespace school.core.features.user.commend.models
{
    public class AddUserCommend : IRequest<Response<string>>
    {

        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string country { get; set; }
        public string phoneNumber { get; set; }
        public string password { get; set; }
        public string confirmpassword { get; set; }

    }
}
