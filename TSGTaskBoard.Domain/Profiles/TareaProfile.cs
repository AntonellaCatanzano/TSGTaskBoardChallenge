using AutoMapper;
using TSGTaskBoard.Domain.DTO;
using TSGTaskBoard.Domain.Entities;

namespace TSGTaskBoard.Domain.Profiles
{
    public class TareaProfile : Profile
    {
        public TareaProfile()
        {
            CreateMap<Tarea, TareaDTO>()
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado.ToString()));

            CreateMap<TareaCreateDTO, Tarea>()
                .ForMember(dest => dest.Estado, opt => opt.Ignore())
                .ForMember(dest => dest.FechaInicio, opt => opt.Ignore());
            
            CreateMap<TareaUpdateDTO, Tarea>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.FechaInicio, opt => opt.Ignore());
        }
    }
}
