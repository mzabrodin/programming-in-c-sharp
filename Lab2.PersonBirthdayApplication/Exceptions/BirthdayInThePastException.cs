namespace Lab2.PersonBirthdayApplication.Exceptions;

public class BirthdayInThePastException : Exception
{
    private const string DefaultMessage = "Birthday date cannot be more than 135 years ago";

    public BirthdayInThePastException() : base(DefaultMessage)
    {
    }
}