using school.Data.Entites.identity;
using school.Data.Helper;
using System.IdentityModel.Tokens.Jwt;

namespace School.Service.Abstract
{
    public interface IAuthentication
    {
        public Task<jwtAuthenticationResult> GetJwtToken(User user);
        public Task<string> ValidateToken(string accessToken);
        public JwtSecurityToken ReadJwtToken(string AcessToken);
        public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string AccessToken, string RefreshToken);

        public Task<jwtAuthenticationResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken);
    }

}
