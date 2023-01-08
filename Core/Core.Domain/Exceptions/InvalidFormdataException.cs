namespace Core.Domain.Exceptions;

public class InvalidFormdataException : Exception
{
    public InvalidFormdataException()
    {
    }

    public InvalidFormdataException(string message)
        : base(message)
    {
    }

    public InvalidFormdataException(string message, Exception inner)
        : base(message, inner)
    {
    }
}