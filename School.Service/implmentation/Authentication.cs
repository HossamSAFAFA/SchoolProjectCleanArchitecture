using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using school.Data.Entites.identity;
using school.Data.Helper;
using school.infrstrcture.Abstract;
using School.Service.Abstract;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace School.Service.implmentation
{
    public class Authentication : IAuthentication
    {
        public UserManager<User> UserManager { get; }
        private readonly IrefrashToken __refrashToken;
        private readonly ConcurrentDictionary<string, Refrashtoken> userRefrashtoken;


        public IConfiguration Config { get; }

        public Authentication(UserManager<User> _userManager, IConfiguration _config, IrefrashToken _refrashToken)
        {
            UserManager = _userManager;
            Config = _config;
            __refrashToken = _refrashToken;
            userRefrashtoken = new ConcurrentDictionary<string, Refrashtoken>();
        }



        public async Task<jwtAuthenticationResult> GetJwtToken(User user)
        {
            var (myToken, acesstoken) = await GenerateJwtToken(user);


            var refrachToken = new Refrashtoken()
            {
                userName = user.UserName,
                ExpiredAt = DateTime.Now.AddDays(7),
                TokeString = GenrateRefrashToken()
            };
            userRefrashtoken.AddOrUpdate(refrachToken.TokeString, refrachToken, (O, S) => refrachToken);
            var UserRefrashToken = new UserRefreshToken()
            {
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(7),
                UserId = user.Id,
                JwtId = myToken.Id,
                IsUsed = false,
                IsRevoked = false,
                RefreshToken = refrachToken.TokeString,
                Token = acesstoken,



            };
            await __refrashToken.AddAsync(UserRefrashToken);

            return new jwtAuthenticationResult() { Accesstoken = acesstoken, refrashtoken = refrachToken };

        }
        private async Task<(JwtSecurityToken, string)> GenerateJwtToken(User user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            var UserRole = await UserManager.GetRolesAsync(user);
            foreach (var Role in UserRole)
            {
                claims.Add(new Claim(ClaimTypes.Role, Role));
            }
            var SignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["JWT:SecritKey"]));
            SigningCredentials signcred = new SigningCredentials(SignKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken myToken = new JwtSecurityToken(
                audience: Config["JWT:AudienceIP"],
                issuer: Config["JWT:IssuerIP"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signcred

                );

            var acesstoken = new JwtSecurityTokenHandler().WriteToken(myToken);
            return (myToken, acesstoken);
        }
        private string GenrateRefrashToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public JwtSecurityToken ReadJwtToken(string AcessToken)
        {
            if (string.IsNullOrEmpty(AcessToken))
            {
                throw new ArgumentNullException();
            }
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(AcessToken);
            return response;
        }

        public async Task<jwtAuthenticationResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken)
        {
            var (jwtSecurityToken, newToken) = await GenerateJwtToken(user);
            var response = new jwtAuthenticationResult();
            response.Accesstoken = newToken;
            var refreshTokenResult = new Refrashtoken();
            refreshTokenResult.userName = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            refreshTokenResult.TokeString = refreshToken;
            refreshTokenResult.ExpiredAt = (DateTime)expiryDate;
            response.refrashtoken = refreshTokenResult;
            return response;

        }

        async Task<string> IAuthentication.ValidateToken(string accessToken)
        {
            //throw new NotImplementedException();
            var handler = new JwtSecurityTokenHandler();

            var paremterValidate = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Config["JWT:IssuerIP"],
                ValidAudience = Config["JWT:AudienceIP"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["JWT:SecritKey"]))
            };
            try
            {
                var validator = handler.ValidateToken(accessToken, paremterValidate, out SecurityToken securityToken);
                if (validator == null)
                {
                    throw new SecurityTokenException("Invaild Token");
                }
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;

            }


        }
        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string acessToken, string refrashToken)
        {
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                return ("AlgorithmIsWrong", null);
            }
            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                return ("TokenIsNotExpired", null);
            }

            //Get User

            var userId = (jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

            var userrefrashToken = await __refrashToken.GetTableNoTracking().FirstOrDefaultAsync(x => x.Token == acessToken && x.RefreshToken == refrashToken && x.UserId == int.Parse(userId));// && x => x.RefreshToken == acessToken);

            if (refrashToken == null)
            {
                return ("RefreshTokenIsNotFound", null);
            }

            if (userrefrashToken.ExpiryDate < DateTime.UtcNow)
            {
                userrefrashToken.IsRevoked = true;
                userrefrashToken.IsUsed = false;
                await __refrashToken.UpdateAsync(userrefrashToken);
                return ("RefreshTokenIsExpired", null);
            }
            var expirydate = userrefrashToken.ExpiryDate;
            return (userId, expirydate);
        }


    }

}
