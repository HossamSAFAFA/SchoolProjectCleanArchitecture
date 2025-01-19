using MediatR;
using school.core.Base;

namespace school.core.features.Authentication.Querys.models
{
    public class AuthorizeUserQuery : IRequest<Response<string>>
    {
        public string AcessToken { get; set; }

    }
}
