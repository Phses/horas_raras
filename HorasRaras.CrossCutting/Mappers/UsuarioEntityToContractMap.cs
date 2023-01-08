using HorasRaras.Domain.Contracts;
using HorasRaras.Domain.Entities;
using AutoMapper;

namespace HorasRaras.CrossCutting.Mappers;

public class UsuarioEntityToContractMap : Profile
{
    public UsuarioEntityToContractMap()
    {
        CreateMap<UsuarioEntity, UsuarioRequest>().ReverseMap();
        CreateMap<UsuarioEntity, UsuarioResponse>().ReverseMap();
    }
}