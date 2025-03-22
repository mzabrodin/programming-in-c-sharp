namespace Lab4.PersonList.Exceptions;

public class NameFormatException : Exception
{
    private const string DefaultMessage = "Name and surname must start with a capital letter";

    public NameFormatException() : base(DefaultMessage)
    {
    }
    
    public NameFormatException(string message) : base(message)
    {
    }
}