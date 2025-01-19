using MediatR;
using school.core.Base;
using school.core.features.Authentication.Querys.models;
using School.Service.Abstract;

namespace school.core.features.Authentication.Querys.Handlers
{
    public class AuthenticationQueryHandlers : ResponseHandler, IRequestHandler<AuthorizeUserQuery, Response<string>>
    {
        private readonly IAuthentication _authenticationService;

        public AuthenticationQueryHandlers(IAuthentication authenticationService)

        {

            _authenticationService = authenticationService;
        }


        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateToken(request.AcessToken);
            if (result == "Success")
                return Success(result);
            return BadRequest<string>("Invaild Token");

        }

        //  async Task<Response<jwtAuthenticationResult>> IRequestHandler<SignInCommend, Response<string>>.Handle(SignInCommend request, CancellationToken cancellationToken)
        //  {
        //    
        //  }
    }
}

