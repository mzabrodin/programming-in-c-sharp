using System.IO;
using System.Text.Json;
using Lab4.PersonList.Models;

namespace Lab4.PersonList.Repositories;

public class PersonRepository
{
    private const string FilePath = "personList.json";
    private readonly List<Person> _personList;
    public static readonly PersonRepository Instance = new();

    private static readonly JsonSerializerOptions SWriteOptions = new()
    {
        WriteIndented = true
    };

    private PersonRepository()
    {
        _personList = LoadFromFile();
    }

    public void AddPerson(Person person)
    {
        _personList.Add(person);
        SaveToFile();
    }

    public void RemovePerson(Person person)
    {
        _personList.Remove(person);
        SaveToFile();
    }

    public void UpdatePerson(string email, Person updatedPerson)
    {
        Person? person = GetPersonByEmail(email);
        _personList[_personList.IndexOf(person)] = updatedPerson;
        SaveToFile();
    }

    public Person? GetPersonByEmail(string email)
    {
        return _personList.FirstOrDefault(p => p.Email == email);
    }

    public List<Person> GetAllPersons()
    {
        return new List<Person>(_personList);
    }

    private void SaveToFile()
    {
        string json = JsonSerializer.Serialize(_personList, SWriteOptions);
        File.WriteAllText(FilePath, json);
    }

    private List<Person> LoadFromFile()
    {
        if (!File.Exists(FilePath))
            return new List<Person>();
        string json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<Person>>(json) ?? new List<Person>();
    }
}