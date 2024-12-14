namespace Hotel.Domain.Exceptions;

public class HotelDomainException : Exception
{
    public HotelDomainException()
    {
    }

    public HotelDomainException(string message) : base(message)
    {
    }
    
    public HotelDomainException(string message, Exception exception): base(message, exception)
    {
    }
}