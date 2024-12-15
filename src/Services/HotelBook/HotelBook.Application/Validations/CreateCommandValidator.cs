using FluentValidation;
using HotelBook.Application.Commands.CreateHotel;

namespace HotelBook.Application.Validations;

public class CreateCommandValidator : AbstractValidator<CreateHotelCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(r => r.HotelName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");
        
        RuleFor(r => r.AuthorizedName)
            .Length(1, 25)
            .WithMessage("{PropertyName} must be between 1 and 25 characters");

        RuleFor(r => r.AuthorizedSurname)
            .Length(1, 25)
            .WithMessage("{PropertyName} must be between 1 and 25 characters");

        RuleFor(r => r.FirmTitle)
            .NotEmpty();

        RuleFor(r => r.PhoneNumber)
            .NotEmpty();

        RuleFor(r => r.Email)
            .NotEmpty();
        
        RuleFor(r => r.Location)
            .NotEmpty();
        
        RuleFor(r => r.Note)
            .NotEmpty();

    }
}