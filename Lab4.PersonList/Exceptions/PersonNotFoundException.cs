namespace Lab4.PersonList.Exceptions;

public class PersonNotFoundException : Exception
{
    private const string DefaultMessage = "Person with this email not found";

    public PersonNotFoundException() : base(DefaultMessage)
    {
    }

    public PersonNotFoundException(string message) : base(message)
    {
    }
}