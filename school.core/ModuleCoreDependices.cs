using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace school.core
{
    public static class ModuleDepandiscore
    {
        //File Edit Format View Help
        //services.AddMediatR(cf-> cfg.RegisterServicesFromssemblies(Assembly.GetExecutingAssembly())):

        public static IServiceCollection AddDepanciescore(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper((Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());



            return services;

        }

    }
}
