using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using School.Service.Abstract;
using School.Service.implmentation;

namespace School.Service
{
    public static class ModuleServiceDepancies
    {

        public static IServiceCollection AddDepanciesService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IStuidentservice, StudentService>();
            services.AddTransient<IAuthentication, Authentication>();
            services.AddTransient<IAuthorize, Authorize>();

            // services.AddAuthentication(option =>
            // {
            //     option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //     option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //     option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //
            //
            // }).AddJwtBearer(o =>
            // {
            //     o.SaveToken = true;
            //     o.RequireHttpsMetadata = false;
            //     o.TokenValidationParameters = new TokenValidationParameters()
            //     {
            //
            //         ValidateIssuer = true,
            //         ValidIssuer = configuration["JWT:IssuerIP"],
            //         ValidateAudience = true,
            //         ValidAudience = configuration["JWT:AudienceIP"],
            //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecritKey"]))
            //
            //
            //     };
            // });


            return services;


        }



    }
}
