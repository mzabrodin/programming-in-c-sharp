using Lab2and3.PersonBirthdayApplication.Exceptions;

namespace Lab2and3.PersonBirthdayApplication.Extensions;

public static class AgeExtensions
{
    public static int GetAge(this DateTime date)
    {
        DateTime today = DateTime.Today;
        int age = today.Year - date.Year;
        if (date.Date > today.AddYears(-age)) age--;

        return age;
    }

    public static bool IsAdult(this DateTime date)
    {
        return date.GetAge() >= 18;
    }

    public static bool IsBirthday(this DateTime date)
    {
        return date.Month == DateTime.Today.Month && date.Day == DateTime.Today.Day;
    }

    public static bool IsValidAge(this DateTime date)
    {
        int age = date.GetAge();
        if (age < 0) throw new BirthdayInTheFutureException();

        if (age > 135) throw new BirthdayInThePastException();

        return true;
    }
}