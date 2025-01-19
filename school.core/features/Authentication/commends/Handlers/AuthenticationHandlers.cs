using MediatR;
using Microsoft.AspNetCore.Identity;
using school.core.Base;
using school.core.features.Authentication.commends.models;
using school.Data.Entites.identity;
using school.Data.Helper;
using School.Service.Abstract;

namespace school.core.features.Authentication.commends.Handlers
{
    public class AuthenticationHandlers : ResponseHandler, IRequestHandler<SignInCommend, Response<jwtAuthenticationResult>>, IRequestHandler<RefrashTokenCommend, Response<jwtAuthenticationResult>>
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IAuthentication authentication;


        public AuthenticationHandlers(UserManager<User> _userManager, SignInManager<User> _signInManager, IAuthentication _authentication)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            authentication = _authentication;

        }

        public async Task<Response<jwtAuthenticationResult>> Handle(SignInCommend request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.Username);
            if (user == null) return BadRequest<jwtAuthenticationResult>("password or username is wrong");
            var isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                return BadRequest<jwtAuthenticationResult>("password or username is wrong");
            }

            var token = await authentication.GetJwtToken(user);

            return Success(token);
        }

        async public Task<Response<jwtAuthenticationResult>> Handle(RefrashTokenCommend request, CancellationToken cancellationToken)
        {
            var token = await authentication.GetRefrashToken(request.AcessToken, request.RefrashToken);
            return Success(token);
        }

        //  async Task<Response<jwtAuthenticationResult>> IRequestHandler<SignInCommend, Response<string>>.Handle(SignInCommend request, CancellationToken cancellationToken)
        //  {
        //    
        //  }
    }
}
