
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using school.core;
using school.infrstrcture;
using school.infrstrcture.Data;
using School.Service;
using System.Globalization;
using System.Text;
namespace SchoolApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddFluentValidation();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext"));
            });
            //Jwt
            // builder.Services.AddAuthentication(option =>
            // {
            //     option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //     option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //     option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //
            //
            // }).AddJwtBearer("Bearer", o =>
            // {
            //     o.SaveToken = true;
            //     o.RequireHttpsMetadata = false;
            //     o.TokenValidationParameters = new TokenValidationParameters()
            //     {
            //
            //         ValidateIssuer = true,
            //         //ValidIssuer = builder.Configuration["JWT:IssuerIP"],
            //         ValidIssuer = "iii",
            //         //configuration["JWT:IssuerIP"],
            //         ValidateAudience = true,
            //         // ValidAudience = builder.Configuration["JWT:AudienceIP"],
            //         ValidAudience = "iii",
            //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("lkslhfhifhoheihoheohoehoheoeh4455@####"))//builder.Configuration["JWT:SecritKey"]))
            //
            //
            //     };
            // });
            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowFrontend", builder =>
            //    {
            //        builder.WithOrigins("http://localhost:4200")
            //               .AllowAnyHeader()
            //               .AllowAnyMethod();
            //    });
            //});
            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //
            //}).AddJwtBearer(options =>
            //{
            //    options.SaveToken = true;
            //    options.RequireHttpsMetadata = false;
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = builder.Configuration["JWT:IssuerIP"],
            //        ValidAudience = builder.Configuration["JWT:AudienceIP"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecritKey"]))
            //    };
            //});
            builder.Services.AddAuthentication("Bearer")
     .AddJwtBearer(options =>
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             ValidIssuer = builder.Configuration["JWT:IssuerIP"],
             ValidAudience = builder.Configuration["JWT:AudienceIP"],
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecritKey"]))
         };
     });
            var provider = builder.Services.BuildServiceProvider();
            var schemeProvider = provider.GetRequiredService<IAuthenticationSchemeProvider>();
            var defaultScheme = schemeProvider.GetDefaultAuthenticateSchemeAsync().Result;

            Console.WriteLine($"Default Authentication Scheme: {defaultScheme?.Name}");

            Console.WriteLine($"Default Authentication Scheme: {builder.Services.BuildServiceProvider().GetRequiredService<IAuthenticationSchemeProvider>().GetDefaultAuthenticateSchemeAsync().Result}");

            Console.WriteLine($"Authentication Scheme: {JwtBearerDefaults.AuthenticationScheme}");
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Bearer", policy =>
                    policy.RequireAuthenticatedUser());
            });



            //
            #region Dependency injection
            builder.Services.AddInfrstcutreDepanice().AddDepanciesService(builder.Configuration).AddDepanciescore().AddServiceRegisteration();
            #endregion
            #region Localization
            builder.Services.AddControllersWithViews();
            builder.Services.AddLocalization(opt => opt.ResourcesPath = "");
            builder.Services.Configure<RequestLocalizationOptions>(opt =>

            {
                List<CultureInfo> supportedCluter = new List<CultureInfo> {
                new CultureInfo("en-Us"),
                new CultureInfo("de-De"),
                new CultureInfo("ar-EG"),
                new CultureInfo("fr-FR"),
                 };


                opt.DefaultRequestCulture = new RequestCulture("en-Us");
                opt.SupportedCultures = supportedCluter;
                opt.SupportedUICultures = supportedCluter;

            }
            );
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);


            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            #region logalization
            var option = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(option.Value);
            #endregion
            ;
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStatusCodePages(context =>
            {
                if (context.HttpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    Console.WriteLine("Not Found: The requested route might not exist.");
                }
                else if (context.HttpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    Console.WriteLine("Unauthorized: Missing token or invalid token.");
                }
                return Task.CompletedTask;
            });
            app.MapControllers();

            app.Run();


            ;
        }
    }
}
