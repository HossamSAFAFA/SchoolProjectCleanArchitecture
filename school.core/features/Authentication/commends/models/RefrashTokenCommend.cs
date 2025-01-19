using MediatR;
using school.core.Base;
using school.Data.Helper;

namespace school.core.features.Authentication.commends.models
{
    public class RefrashTokenCommend : IRequest<Response<jwtAuthenticationResult>>
    {
        public string AcessToken { get; set; }
        public string RefrashToken { get; set; }

    }
}
