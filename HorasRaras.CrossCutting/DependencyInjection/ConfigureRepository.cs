using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using HorasRaras.Domain.Interfaces;
using HorasRaras.Data.Repository;
using HorasRaras.Data.Context;

namespace HorasRaras.CrossCutting.DependencyInjection;

public static class ConfigureRepository
{
    public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddScoped<IClienteRepository, ClienteRepository>();
        serviceCollection.AddScoped<IPerfilRepository, PerfilRepository>();
        serviceCollection.AddScoped<IProjetoRepository, ProjetoRepository>();
        serviceCollection.AddScoped<ITarefaRepository, TarefaRepository>();
        serviceCollection.AddScoped<IUsuarioRepository, UsuarioRepository>();
        serviceCollection.AddScoped<IUsuarioProjetoRepository, UsuarioProjetoRepository>();
        serviceCollection.AddDbContext<HorasRarasContext>(options => {
            options.UseSqlServer(connectionString);
            options.UseLazyLoadingProxies();
        });
    }
}