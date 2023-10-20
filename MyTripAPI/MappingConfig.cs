using AutoMapper;
using MyTripAPI.Model.DTO;
using MyTripAPI.Model;

namespace MyTripAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {

            CreateMap<Cabin, CabinDTO>();
            CreateMap<CabinDTO, Cabin>();

            CreateMap<Cabin, CabinCreateDTO>().ReverseMap();
            CreateMap<Cabin, CabinUpdateDTO>().ReverseMap();
        }

    }
}
