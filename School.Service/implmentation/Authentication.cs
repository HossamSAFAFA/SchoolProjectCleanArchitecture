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
        private JwtSecurityToken ReadJwtToken(string AcessToken)
        {
            if (string.IsNullOrEmpty(AcessToken))
            {
                throw new ArgumentNullException();
            }
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(AcessToken);
            return response;
        }
        async Task<jwtAuthenticationResult> IAuthentication.GetRefrashToken(string acessToken, string refrashToken)
        {
            var jwtToken = ReadJwtToken(acessToken);
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
            {
                throw new SecurityTokenException("Algorithm Is Wrong");

            }
            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                throw new SecurityTokenException("Token Is Not Expired");

            }
            if (refrashToken == null)
            {
                throw new SecurityTokenException("Refrashtoken is null");

            }
            var userId = (jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

            var userrefrashToken = await __refrashToken.GetTableNoTracking().FirstOrDefaultAsync(x => x.Token == acessToken && x.RefreshToken == refrashToken && x.UserId == int.Parse(userId));// && x => x.RefreshToken == acessToken);
            if (userrefrashToken.ExpiryDate < DateTime.UtcNow)
            {
                throw new SecurityTokenException("Reefrash token is expired");
            }
            var user = await UserManager.FindByIdAsync(userId);
            if (user == null)
                throw new SecurityTokenException("user not found");

            var (jwttoken, newtoken) = await GenerateJwtToken(user);
            var refrashtoken = new Refrashtoken();
            refrashtoken.ExpiredAt = userrefrashToken.ExpiryDate;
            refrashtoken.TokeString = refrashToken;
            refrashtoken.userName = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name).Value;
            return new jwtAuthenticationResult() { Accesstoken = newtoken, refrashtoken = refrashtoken };

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
            var validator = handler.ValidateToken(accessToken, paremterValidate, out SecurityToken securityToken);
            try
            {
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


    }

}
