using school.Data.Entites.identity;
using school.Data.Helper;

namespace School.Service.Abstract
{
    public interface IAuthentication
    {
        public Task<jwtAuthenticationResult> GetJwtToken(User user);
        public Task<jwtAuthenticationResult> GetRefrashToken(string acessToken, string refrashToken);
        public Task<string> ValidateToken(string accessToken);

    }

}
