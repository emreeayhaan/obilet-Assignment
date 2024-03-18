using AutoMapper;
using obilet_Assignment.Models;
using ObiletData.Dto;

namespace obilet_Assignment.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() {

            CreateMap<Journey, JourneyModel>()
                .ForMember(dest => dest.Arrival, opt => opt.MapFrom(src => src.Arrival.ToString("HH:mm")))
                .ForMember(dest => dest.Departure, opt => opt.MapFrom(src => src.Departure.ToString("HH:mm")))
                .ReverseMap();
            CreateMap<JourneysResponseDto, IndexViewJourneyModel>()
                .ForMember(dest => dest.Journeys, opt => opt.MapFrom(src => src.Data.Select(x => x.Journey).ToList()))
                .ReverseMap();
        } 
    }
}
