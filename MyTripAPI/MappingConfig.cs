using AutoMapper;
using MyTripAPI.Model.DTO;
using MyTripAPI.Model;

namespace MyTripAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {

            CreateMap<Suite, SuiteDTO>();
            CreateMap<SuiteDTO, Suite>();

            CreateMap<Suite, SuiteCreateDTO>().ReverseMap();
            CreateMap<Suite, SuiteUpdateDTO>().ReverseMap();
        }

    }
}
