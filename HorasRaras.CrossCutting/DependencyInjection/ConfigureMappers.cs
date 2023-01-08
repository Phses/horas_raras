using Microsoft.Extensions.DependencyInjection;
using HorasRaras.CrossCutting.Mappers;
using AutoMapper;

namespace HorasRaras.CrossCutting.DependencyInjection;

public static class ConfigureMappers
{
    public static void ConfigureDependenciesMappers(IServiceCollection serviceCollection)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ClienteEntityToContractMap());
            cfg.AddProfile(new PerfilEntityToContractMap());
            cfg.AddProfile(new ProjetoEntityToContractMap());
            cfg.AddProfile(new TarefaEntityToContractMap());
            cfg.AddProfile(new UsuarioEntityToContractMap());
            cfg.AddProfile(new ToggleTaskToTarefaTogglResponseContractMap());
        });

        var mapConfiguration = config.CreateMapper();
        serviceCollection.AddSingleton(mapConfiguration);
    }
}