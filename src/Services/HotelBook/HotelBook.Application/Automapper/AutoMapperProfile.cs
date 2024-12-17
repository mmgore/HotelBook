using AutoMapper;
using HotelBook.Application.Quaries;
using HotelBook.Domain.AggregatesModel.HotelAggregate;

namespace HotelBook.Application.Automapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AuthorizedPersonListDto, Hotel>()
            .ReverseMap();
    }
}