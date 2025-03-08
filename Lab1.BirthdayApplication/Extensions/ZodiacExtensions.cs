using Lab1.BirthdayApplication.Models;

namespace Lab1.BirthdayApplication.Extensions;

public static class ZodiacExtensions
{
    public static WesternZodiac GetWesternZodiac(this DateTime date)
    {
        int day = date.Day;
        int month = date.Month;

        return month switch
        {
            1 => day <= 20 ? WesternZodiac.Capricorn : WesternZodiac.Aquarius,
            2 => day <= 19 ? WesternZodiac.Aquarius : WesternZodiac.Pisces,
            3 => day <= 20 ? WesternZodiac.Pisces : WesternZodiac.Aries,
            4 => day <= 20 ? WesternZodiac.Aries : WesternZodiac.Taurus,
            5 => day <= 21 ? WesternZodiac.Taurus : WesternZodiac.Gemini,
            6 => day <= 21 ? WesternZodiac.Gemini : WesternZodiac.Cancer,
            7 => day <= 22 ? WesternZodiac.Cancer : WesternZodiac.Leo,
            8 => day <= 23 ? WesternZodiac.Leo : WesternZodiac.Virgo,
            9 => day <= 23 ? WesternZodiac.Virgo : WesternZodiac.Libra,
            10 => day <= 23 ? WesternZodiac.Libra : WesternZodiac.Scorpio,
            11 => day <= 22 ? WesternZodiac.Scorpio : WesternZodiac.Sagittarius,
            12 => day <= 21 ? WesternZodiac.Sagittarius : WesternZodiac.Capricorn,
            _ => throw new ArgumentOutOfRangeException(nameof(date), date, null)
        };
    }

    public static ChineseZodiac GetChineseZodiac(this DateTime date)
    {
        int year = date.Year;
        return ((year - 4) % 12) switch
        {
            0 => ChineseZodiac.Rat,
            1 => ChineseZodiac.Ox,
            2 => ChineseZodiac.Tiger,
            3 => ChineseZodiac.Rabbit,
            4 => ChineseZodiac.Dragon,
            5 => ChineseZodiac.Snake,
            6 => ChineseZodiac.Horse,
            7 => ChineseZodiac.Goat,
            8 => ChineseZodiac.Monkey,
            9 => ChineseZodiac.Rooster,
            10 => ChineseZodiac.Dog,
            11 => ChineseZodiac.Pig,
            _ => throw new ArgumentOutOfRangeException(nameof(date), date, null)
        };
    }
}