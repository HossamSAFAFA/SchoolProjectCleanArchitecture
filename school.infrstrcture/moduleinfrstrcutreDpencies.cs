using Microsoft.Extensions.DependencyInjection;
using school.infrstrcture.Abstract;
using school.infrstrcture.Baseinfrstcture;
using school.infrstrcture.Repositary;

namespace school.infrstrcture
{
    public static class ModuleinfrstrcutreDpenciesinjection
    {
        public static IServiceCollection AddInfrstcutreDepanice(this IServiceCollection Service)
        {
            Service.AddTransient<IStudentRepository, StudentRepository>();
            Service.AddTransient<IrefrashToken, RefreshTokenRepository>();
            Service.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));

            return Service;
        }

    }
}
