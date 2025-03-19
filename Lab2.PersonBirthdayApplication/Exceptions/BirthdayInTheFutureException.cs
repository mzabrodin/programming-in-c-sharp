namespace Lab2.PersonBirthdayApplication.Exceptions;

public class BirthdayInTheFutureException : Exception
{
    private const string DefaultMessage = "Birthday date cannot be in the future";

    public BirthdayInTheFutureException() : base(DefaultMessage)
    {
    }
}