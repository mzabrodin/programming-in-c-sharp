namespace Lab2.PersonBirthdayApplication.Extensions;

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

    public static int IsValidAge(this DateTime date)
    {
        int age = date.GetAge();
        if (age < 0) return 1;

        if (age > 135) return -1;

        return 0;
    }
}