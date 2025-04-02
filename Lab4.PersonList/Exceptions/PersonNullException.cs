namespace Lab4.PersonList.Exceptions;

public class PersonNullException : Exception
{
    private const string DefaultMessage = "Person cannot be null";

    public PersonNullException() : base(DefaultMessage)
    {
    }
}