using AutoMapper;
using HorasRaras.Domain.Contracts.Request;
using HorasRaras.Domain.Contracts.Response;
using HorasRaras.Domain.Entities;

namespace HorasRaras.CrossCutting.Mappers
{
    public class ClienteEntityToContractMap : Profile
    {
        public ClienteEntityToContractMap()
        {
            CreateMap<ClienteEntity, ClienteRequest>().ReverseMap();
            CreateMap<ClienteEntity, ClienteResponse>().ReverseMap();
        }
    }
}
