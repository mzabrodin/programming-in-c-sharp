using System.Text.Json.Serialization;
using Lab4.PersonList.Extensions;

namespace Lab4.PersonList.Models;

public class Person
{
    [JsonConstructor]
    public Person(string name, string surname, string email, DateTime birthday)
    {
        Name = name;
        Surname = surname;
        Email = email;
        Birthday = birthday;
        InitProps();
    }

    public Person(string name, string surname, string email)
        : this(name, surname, email, DateTime.Now)
    {
    }

    public Person(string name, string surname, DateTime birthday)
        : this(name, surname, String.Empty, birthday)
    {
    }

    public string Name { get; private set; }

    public string Surname { get; private set; }

    public string Email { get; private set; }

    public DateTime Birthday { get; private set; }
    public bool IsAdult { get; private set; }

    public WesternZodiac SunSign { get; private set; }

    public ChineseZodiac ChineseSign { get; private set; }

    public bool IsBirthday { get; private set; }

    public int Age { get; private set; }

    public void InitProps()
    {
        IsAdult = Birthday.IsAdult();
        SunSign = Birthday.GetWesternZodiac();
        ChineseSign = Birthday.GetChineseZodiac();
        IsBirthday = Birthday.IsBirthday();
        Age = Birthday.GetAge();
    }
}