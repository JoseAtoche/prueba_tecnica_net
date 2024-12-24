namespace PruebaTecnica.Application.Mappers;

public class BankMapper : Profile
{
    public BankMapper()
    {
        CreateMap<BankEntity, BankDto>();
        CreateMap<BankDto, BankEntity>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
            .ForMember(dest => dest.BIC, opt => opt.MapFrom(src => src.bic))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.country));
    }
}
