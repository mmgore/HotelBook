using AutoMapper;
using HotelBook.Domain.AggregatesModel.HotelAggregate;
using MediatR;

namespace HotelBook.Application.Quaries;

public class GetAuthorizedPersonListQueryHandler : IRequestHandler<GetAuthorizedPersonListQuery, AuthorizedPersonListViewModel>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IMapper _mapper;

    public GetAuthorizedPersonListQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
    {
        _hotelRepository = hotelRepository ?? throw new ArgumentNullException(nameof(hotelRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<AuthorizedPersonListViewModel> Handle(GetAuthorizedPersonListQuery request, CancellationToken cancellationToken)
    {
        return new AuthorizedPersonListViewModel
        {
            AuthorizedPersonList = _mapper.Map<IEnumerable<AuthorizedPersonListDto>>(await _hotelRepository.GetHotels())
        };
    }
}

