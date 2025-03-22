using System.Net.Mail;
using Lab4.PersonList.Exceptions;

namespace Lab4.PersonList.Extensions;

public static class EmailExtension
{
    public static bool IsEmail(this string email)
    {
        if (email.EndsWith('.'))
        {
            throw new EmailFormatException();
        }
        
        try
        {
            var mailAddress = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            throw new EmailFormatException();
        }
    }
}