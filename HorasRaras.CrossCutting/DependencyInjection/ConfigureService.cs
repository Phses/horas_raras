using Microsoft.Extensions.DependencyInjection;
using HorasRaras.Domain.Interfaces;
using HorasRaras.Service;
using HorasRaras.Domain.Interfaces.Service;

namespace HorasRaras.CrossCutting.DependencyInjection;

public static class ConfigureService
{
    public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IClienteService, ClienteService>();
        serviceCollection.AddScoped<IPerfilService, PerfilService>();
        serviceCollection.AddScoped<IProjetoService, ProjetoService>();
        serviceCollection.AddScoped<ITarefaService, TarefaService>();
        serviceCollection.AddScoped<IEmailService, EmailService>();
        serviceCollection.AddScoped<ITogglService, TogglService>();
        serviceCollection.AddScoped<IUsuarioService, UsuarioService>();
    }
}