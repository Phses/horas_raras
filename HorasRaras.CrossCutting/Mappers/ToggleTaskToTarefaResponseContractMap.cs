using AutoMapper;
using HorasRaras.Domain.Contracts.Response;
using HorasRaras.Domain.Entities;


namespace HorasRaras.CrossCutting.Mappers
{
    public class ToggleTaskToTarefaTogglResponseContractMap : Profile
    {
        public ToggleTaskToTarefaTogglResponseContractMap()
        {
            CreateMap<Datum, TarefaTogglResponse>()
                .ForMember(dst => dst.Descricao, map => map.MapFrom(src => src.Description))
                .ForMember(dst => dst.HoraInicio, map => map.MapFrom(src => src.Start.Value.DateTime))
                .ForMember(dst => dst.HoraFinal, map => map.MapFrom(src => src.End.Value.DateTime)).ReverseMap();
        }
    }
}
