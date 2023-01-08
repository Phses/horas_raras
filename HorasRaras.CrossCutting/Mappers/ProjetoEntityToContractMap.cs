using AutoMapper;
using HorasRaras.Domain.Contracts.Request;
using HorasRaras.Domain.Contracts.Response;
using HorasRaras.Domain.Entities;

namespace HorasRaras.CrossCutting.Mappers
{
    public class ProjetoEntityToContractMap : Profile
    {
        public ProjetoEntityToContractMap()
        {
            CreateMap<ProjetoEntity, ProjetoRequest>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<ProjetoEntity, ProjetoResponse>().ReverseMap();
        }
    }
}
