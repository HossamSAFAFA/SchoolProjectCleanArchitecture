using MediatR;
using school.core.Base;
using school.Data.Helper;

namespace school.core.features.Authentication.commends.models
{
    public class SignInCommend : IRequest<Response<jwtAuthenticationResult>>
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
