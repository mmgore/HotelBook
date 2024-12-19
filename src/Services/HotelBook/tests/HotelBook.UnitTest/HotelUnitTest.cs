using AutoMapper;
using FluentAssertions;
using HotelBook.Application.Automapper;
using HotelBook.Application.Commands.CreateHotel;
using HotelBook.Application.Commands.CreateHotelInfo;
using HotelBook.Application.Commands.DeleteHotel;
using HotelBook.Application.Exceptions;
using HotelBook.Application.Validations;
using HotelBook.Domain.AggregatesModel.HotelAggregate;
using HotelBook.Domain.Exceptions;
using HotelBook.Domain.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;

namespace HotelBook.UnitTest;

public class HotelUnitTest
{
    private readonly Mock<IHotelRepository> _mockHotelRepository;
    private readonly Mock<IHotelInformationRepository> _mockHotelInformationRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly IMapper _mapper;

    public HotelUnitTest()
    {
        _mockHotelRepository = new Mock<IHotelRepository>();
        _mockHotelInformationRepository = new Mock<IHotelInformationRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<AutoMapperProfile>();
        });
        _mapper = mapperConfig.CreateMapper();
    }


    [Fact]
    public void Create_hotel_success()
    {
        var sut = Hotel.Create(hotelName, authorizedName, authorizedSurname,firmTitle);
        sut.Should().NotBeNull();
        
    }

    [Fact]
    public void Create_hotel_fail()
    {
        Action sut = () => Hotel.Create(nullHotelName, authorizedName, authorizedSurname, firmTitle);
        sut.Should().Throw<HotelDomainException>()
            .WithMessage("Hotel name cannot be null");
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("authorizedName", true)]
    public void Validate_create_hotel_command(string authorizedName, bool expected)
    {
        var validator = new CreateHotelCommandValidator();

        var cmd = new CreateHotelCommand()
        {
            AuthorizedName = authorizedName, AuthorizedSurname = fakeHotel.AuthorizedSurname,
            Email = fakeHotelInformation.Email, FirmTitle = fakeHotel.FirmTitle, HotelName = fakeHotel.HotelName,
            Location = fakeHotelInformation.Location, Note = fakeHotelInformation
                .Note,
            PhoneNumber = fakeHotelInformation.PhoneNumber
        };
        
        var sut = validator.Validate(cmd);
        sut.IsValid.Should().Be(expected);
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("email", true)]
    public void Validate_create_hotelinfo_command(string email, bool expected)
    {
        var validator = new CreateHotelInfoCommandValidator();
        var cmd = new CreateHotelInfoCommand()
        {
            Email = email, Location = fakeHotelInformation.Location, Note = fakeHotelInformation.Note,
            PhoneNumber = fakeHotelInformation.PhoneNumber, HotelId = fakeHotel.Id
        };
        
        var sut = validator.Validate(cmd);
        sut.IsValid.Should().Be(expected);
    }

    [Fact]
    public async void Should_create_hotel_successful()
    {
        _mockHotelRepository.Setup(r => r.InsertAsync(fakeHotel));
        _mockHotelInformationRepository.Setup(r => r.InsertAsync(fakeHotelInformation));
        var loggerMock = new Mock<ILogger<CreateHotelCommandHandler>>();
        
        var cmd = new CreateHotelCommand()
        {
            AuthorizedName = fakeHotel.AuthorizedName, AuthorizedSurname = fakeHotel.AuthorizedSurname,
            Email = fakeHotelInformation.Email, FirmTitle = fakeHotel.FirmTitle, HotelName = fakeHotel.HotelName,
            Location = fakeHotelInformation.Location, Note = fakeHotelInformation
                .Note,
            PhoneNumber = fakeHotelInformation.PhoneNumber
        };
        
        var handler = new CreateHotelCommandHandler(_mockHotelRepository.Object, _mockHotelInformationRepository.Object ,_mockUnitOfWork.Object, loggerMock.Object);
        
        var sut = await handler.Handle(cmd, CancellationToken.None);
        sut.Should().Be(Unit.Value);
    }

    [Fact]
    public async void Should_delete_hotel_successful()
    {
        var hotel = fakeHotel;
        _mockHotelRepository.Setup(r => r.GetHotelById(hotel.Id))
            .ReturnsAsync(hotel);
        _mockHotelRepository.Setup(r => r.DeleteAsync(hotel));
        var loggerMock = new Mock<ILogger<DeleteHotelCommandHandler>>();

        var cmd = new DeleteHotelCommand(hotel.Id);
        var handler = new DeleteHotelCommandHandler(_mockHotelRepository.Object, _mockUnitOfWork.Object, loggerMock.Object);
        
        var sut = await handler.Handle(cmd, CancellationToken.None);
        sut.Should().Be(Unit.Value);
    }

    [Fact]
    public async void Should_delete_hotel_throws_exception()
    {
        var hotel = fakeHotel;
        _mockHotelRepository.Setup(r => r.GetHotelById(hotel.Id))
            .ReturnsAsync((Hotel)null);
        var loggerMock = new Mock<ILogger<DeleteHotelCommandHandler>>();
        
        var cmd = new DeleteHotelCommand(hotel.Id);
        var handler = new DeleteHotelCommandHandler(_mockHotelRepository.Object, _mockUnitOfWork.Object, loggerMock.Object);
        
        Func<Task> sut = async () => await handler.Handle(cmd, CancellationToken.None);

        sut.Should().ThrowAsync<NotFoundException>();
    }
    
    private Hotel fakeHotel => Hotel.Create(hotelName, authorizedName, authorizedSurname, firmTitle);
    private HotelInformation fakeHotelInformation => HotelInformation.Create(fakeHotel.Id, phoneNumber, email, location, note);
    private string hotelName => "fakeHotelName";
    private string authorizedName => "fakeAuthorizedName";
    private string authorizedSurname => "fakeAuthorizedSurname";
    private string firmTitle => "fakeFirmTitle";
    private string phoneNumber => "fakePhoneNumber";
    private string email => "fakeEmail";
    private string location => "fakeLocation";
    private string note => "fakeNote";
    private string nullHotelName => "";
}