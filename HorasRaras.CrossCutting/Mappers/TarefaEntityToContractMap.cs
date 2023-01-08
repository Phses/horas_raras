using AutoMapper;
using HorasRaras.Domain.Contracts.Request;
using HorasRaras.Domain.Contracts.Response;
using HorasRaras.Domain.Entities;

namespace HorasRaras.CrossCutting.Mappers
{
    public class TarefaEntityToContractMap : Profile
    {
        public TarefaEntityToContractMap()
        {
            CreateMap<TarefaEntity, TarefaRequest>().ReverseMap();
            CreateMap<TarefaEntity, TarefaTogglResponse>().ReverseMap();
            CreateMap<TarefaEntity, TarefaResponse>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
        
    }
}
