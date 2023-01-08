using HorasRaras.CrossCutting.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;


namespace HorasRaras.IoC;

public static class NativeInjectorBootStrapper
{
    public static void RegisterAppDependencies(this IServiceCollection services)
    {
        ConfigureService.ConfigureDependenciesService(services);
        ConfigureMappers.ConfigureDependenciesMappers(services);
    }

    public static void RegisterAppDependenciesContext(this IServiceCollection services, string connectionString)
    {
        ConfigureRepository.ConfigureDependenciesRepository(services, connectionString);
    }
}
