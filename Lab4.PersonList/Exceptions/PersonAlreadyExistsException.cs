namespace Lab4.PersonList.Exceptions;

public class PersonAlreadyExistsException : Exception
{
    private static readonly string DefaultMessage = "Person with this email already exist";

    public PersonAlreadyExistsException() : base(DefaultMessage)
    {
    }

    public PersonAlreadyExistsException(string message) : base(message)
    {
    }
}