using AutoMapper;
using HorasRaras.Domain.Contracts.Request;
using HorasRaras.Domain.Contracts.Response;
using HorasRaras.Domain.Entities;

namespace HorasRaras.CrossCutting.Mappers
{
    public class PerfilEntityToContractMap : Profile
    {
        public PerfilEntityToContractMap()
        {
            CreateMap<PerfilEntity, PerfilRequest>().ReverseMap();
            CreateMap<PerfilEntity, PerfilResponse>().ReverseMap();
        }
    }
}
