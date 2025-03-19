using Lab2and3.PersonBirthdayApplication.Exceptions;

namespace Lab2and3.PersonBirthdayApplication.Extensions;

public static class NameFormatExtension
{
    public static bool ValidNameLength(this string name)
    {
        if (name.Length is >= 2 and <= 50)
        {
            return true;
        }
        else
        {
            throw new NameFormatException("Both name and surname must be between 2 and 50 characters long");
        }
    }
    
    public static bool StartsWithCapitalLetter(this string name)
    {
        if (Char.IsUpper(name[0]))
        {
            return true;
        }
        else
        {
            throw new NameFormatException("Both name and surname must start with a capital letter");
        }
    }
    
    public static bool ContainsOnlyLetters(this string name)
    {
        if (name.All(Char.IsLetter))
        {
            return true;
        }
        else
        {
            throw new NameFormatException("Both name and surname must contain only letters");
        }
    }
}