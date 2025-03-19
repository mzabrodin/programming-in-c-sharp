using Lab2and3.PersonBirthdayApplication.Models;

namespace Lab2and3.PersonBirthdayApplication.Extensions;

public static class ZodiacExtensions
{
    public static WesternZodiac GetWesternZodiac(this DateTime date)
    {
        int day = date.Day;
        int month = date.Month;

        switch (month)
        {
            case 1:
                return day <= 20 ? WesternZodiac.Capricorn : WesternZodiac.Aquarius;
            case 2:
                return day <= 19 ? WesternZodiac.Aquarius : WesternZodiac.Pisces;
            case 3:
                return day <= 20 ? WesternZodiac.Pisces : WesternZodiac.Aries;
            case 4:
                return day <= 20 ? WesternZodiac.Aries : WesternZodiac.Taurus;
            case 5:
                return day <= 21 ? WesternZodiac.Taurus : WesternZodiac.Gemini;
            case 6:
                return day <= 21 ? WesternZodiac.Gemini : WesternZodiac.Cancer;
            case 7:
                return day <= 22 ? WesternZodiac.Cancer : WesternZodiac.Leo;
            case 8:
                return day <= 23 ? WesternZodiac.Leo : WesternZodiac.Virgo;
            case 9:
                return day <= 23 ? WesternZodiac.Virgo : WesternZodiac.Libra;
            case 10:
                return day <= 23 ? WesternZodiac.Libra : WesternZodiac.Scorpio;
            case 11:
                return day <= 22 ? WesternZodiac.Scorpio : WesternZodiac.Sagittarius;
            case 12:
                return day <= 21 ? WesternZodiac.Sagittarius : WesternZodiac.Capricorn;
            default:
                throw new ArgumentOutOfRangeException(nameof(date), date, null);
        }
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