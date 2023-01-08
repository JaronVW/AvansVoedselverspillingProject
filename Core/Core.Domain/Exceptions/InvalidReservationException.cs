namespace Core.Domain.Exceptions;

public class InvalidReservationException : Exception
{
    public InvalidReservationException()
    {
    }

    public InvalidReservationException(string message)
        : base(message)
    {
    }

    public InvalidReservationException(string message, Exception inner)
        : base(message, inner)
    {
    }
}