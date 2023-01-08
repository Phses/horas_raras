using AutoMapper;
using HorasRaras.CrossCutting.Mappers;

namespace HorasRaras.Testes.Configs;

public static class MapConfig
{
    public static IMapper Get()
    {
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ClienteEntityToContractMap());
            cfg.AddProfile(new PerfilEntityToContractMap());
            cfg.AddProfile(new ProjetoEntityToContractMap());
            cfg.AddProfile(new TarefaEntityToContractMap());
            cfg.AddProfile(new UsuarioEntityToContractMap());
        });

        return mockMapper.CreateMapper();
    }
}
