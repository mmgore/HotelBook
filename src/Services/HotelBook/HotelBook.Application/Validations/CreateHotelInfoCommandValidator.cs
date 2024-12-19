using FluentValidation;
using HotelBook.Application.Commands.CreateHotelInfo;

namespace HotelBook.Application.Validations;

public class CreateHotelInfoCommandValidator : AbstractValidator<CreateHotelInfoCommand>
{
    public CreateHotelInfoCommandValidator()
    {
        RuleFor(r => r.Location)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");
        
        RuleFor(r => r.PhoneNumber)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");
        
        RuleFor(r => r.Email)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");
        
        RuleFor(r => r.Note)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");
    }
}