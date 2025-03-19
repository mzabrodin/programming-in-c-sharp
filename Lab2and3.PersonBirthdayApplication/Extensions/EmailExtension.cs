using System.Net.Mail;
using System.Text.RegularExpressions;
using Lab2and3.PersonBirthdayApplication.Exceptions;

namespace Lab2and3.PersonBirthdayApplication.Extensions;

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