namespace Lab4.PersonList.Exceptions;

public class BirthdayInTheFutureException : Exception
{
    private const string DefaultMessage = "Birthday date cannot be in the future";

    public BirthdayInTheFutureException() : base(DefaultMessage)
    {
    }
}