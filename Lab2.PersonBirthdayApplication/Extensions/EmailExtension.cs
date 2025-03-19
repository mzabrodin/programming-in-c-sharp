using System.Net.Mail;
using System.Text.RegularExpressions;
using Lab2.PersonBirthdayApplication.Exceptions;

namespace Lab2.PersonBirthdayApplication.Extensions;

public static class EmailExtension
{
    public static bool IsEmail(this string email)
    {
        try
        {
            var mailAddress = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}