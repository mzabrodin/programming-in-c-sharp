namespace Lab2.PersonBirthdayApplication.Exceptions;

public class EmailFormatException : Exception
{
    private const string DefaultMessage = "Invalid email address format";

    public EmailFormatException() : base(DefaultMessage)
    {
    }
}