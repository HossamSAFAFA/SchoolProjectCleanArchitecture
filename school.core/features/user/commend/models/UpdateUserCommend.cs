using MediatR;
using school.core.Base;

namespace school.core.features.user.commend.models
{
    public class UpdateUserCommend : IRequest<Response<string>>
    {
        public int id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string country { get; set; }
        public string phoneNumber { get; set; }

    }
}
