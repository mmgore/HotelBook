using MediatR;

namespace HotelBook.Application.Quaries;

public class GetAuthorizedPersonListQuery : IRequest<AuthorizedPersonListViewModel>
{
}

public class AuthorizedPersonListDto
{
    public string HotelName { get; set; }
    public string AuthorizedName { get; set; }
    public string AuthorizedSurname { get; set; }
    
}

public class AuthorizedPersonListViewModel
{
    public IEnumerable<AuthorizedPersonListDto> AuthorizedPersonList { get; set; }
}