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
        if (_personList.Count == 0)
        {
            _personList = InitialRepository();
            SaveToFile();
        }
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
    
    private List<Person> InitialRepository()
        {
            return
            [
                new Person("Faith", "Long", "in.lobortis@google.couk", new DateTime(1986, 9, 15)),
                new Person("Clare", "Schroeder", "elit.pede@outlook.edu", new DateTime(1988, 11, 4)),
                new Person("Venus", "Marshall", "integer.mollis@google.ca", new DateTime(1989, 2, 1)),
                new Person("Sheila", "Campos", "nunc.mauris.elit@google.com", new DateTime(2010, 11, 23)),
                new Person("Charles", "Perry", "vel.turpis@icloud.com", new DateTime(1983, 6, 12)),
                new Person("Keith", "Hebert", "nisi.nibh@protonmail.couk", new DateTime(2008, 3, 1)),
                new Person("Preston", "Sanders", "dolor.donec@hotmail.couk", new DateTime(1975, 3, 19)),
                new Person("Willa", "Conner", "magna.et@yahoo.edu", new DateTime(1956, 3, 7)),
                new Person("Nicholas", "Alvarez", "non.nisi@yahoo.ca", new DateTime(1989, 4, 1)),
                new Person("Lucian", "Le", "luctus.ipsum@yahoo.org", new DateTime(1962, 4, 20)),
                new Person("Uriah", "Scott", "metus.eu@google.ca", new DateTime(1976, 9, 5)),
                new Person("Tatiana", "Velez", "et.rutrum@hotmail.com", new DateTime(1960, 3, 30)),
                new Person("Hall", "Randolph", "fringilla.purus@aol.net", new DateTime(1992, 3, 4)),
                new Person("Acton", "Page", "a.nunc@google.ca", new DateTime(1980, 7, 6)),
                new Person("Kareem", "Owen", "ut@yahoo.com", new DateTime(1976, 1, 21)),
                new Person("Sophia", "Frazier", "consequat.nec.mollis@google.org", new DateTime(1961, 7, 30)),
                new Person("Howard", "Horne", "auctor.non@yahoo.couk", new DateTime(2010, 4, 3)),
                new Person("Emerson", "Doyle", "cubilia@hotmail.org", new DateTime(2003, 5, 27)),
                new Person("Harding", "Cote", "purus@protonmail.com", new DateTime(1991, 10, 30)),
                new Person("Wang", "Ingram", "non@outlook.org", new DateTime(1976, 11, 19)),
                new Person("Jescie", "Leonard", "neque.sed@aol.net", new DateTime(2012, 5, 27)),
                new Person("Sylvester", "Christensen", "lacinia.at@protonmail.ca", new DateTime(1962, 9, 19)),
                new Person("Macon", "Foster", "mattis.integer@google.org", new DateTime(2000, 4, 8)),
                new Person("Elizabeth", "Shaffer", "neque.non@hotmail.org", new DateTime(1963, 7, 10)),
                new Person("Ashton", "Saunders", "dui@hotmail.ca", new DateTime(2019, 8, 14)),
                new Person("Kai", "Cline", "ac.mattis.velit@protonmail.ca", new DateTime(1954, 4, 20)),
                new Person("Carl", "Wilson", "morbi.sit@yahoo.net", new DateTime(1968, 2, 18)),
                new Person("Courtney", "Dalton", "morbi.sit.amet@yahoo.com", new DateTime(1972, 6, 19)),
                new Person("Keegan", "Gilbert", "nunc.quisque@google.org", new DateTime(1980, 10, 12)),
                new Person("Kalia", "Collins", "ligula.nullam@icloud.couk", new DateTime(1994, 8, 8)),
                new Person("Alvin", "Lewis", "phasellus@aol.edu", new DateTime(2009, 4, 3)),
                new Person("Henry", "Sosa", "tellus.eu.augue@protonmail.com", new DateTime(1971, 6, 26)),
                new Person("Deirdre", "Crosby", "aliquet@yahoo.couk", new DateTime(2011, 11, 18)),
                new Person("Isaac", "Wilson", "tellus@google.org", new DateTime(1967, 8, 22)),
                new Person("Allen", "Thornton", "sit.amet@protonmail.edu", new DateTime(2009, 6, 8)),
                new Person("Kimberly", "Noel", "tincidunt.aliquam@icloud.com", new DateTime(1983, 9, 20)),
                new Person("Wayne", "Blackwell", "suspendisse.non@protonmail.edu", new DateTime(1962, 8, 15)),
                new Person("Octavia", "Tillman", "felis@aol.edu", new DateTime(1978, 7, 13)),
                new Person("Thomas", "Foster", "dapibus@google.ca", new DateTime(1958, 11, 2)),
                new Person("Shafira", "Ware", "sem@aol.ca", new DateTime(2006, 5, 5)),
                new Person("Ella", "Fitzgerald", "justo.nec.ante@protonmail.couk", new DateTime(1981, 1, 6)),
                new Person("Evan", "Nixon", "vel.sapien@yahoo.org", new DateTime(1982, 12, 18)),
                new Person("India", "William", "nunc.sed.orci@google.com", new DateTime(1997, 5, 17)),
                new Person("Carol", "Kirby", "orci.lacus@outlook.ca", new DateTime(2007, 7, 16)),
                new Person("Oleg", "Lara", "ut@yahoo.edu", new DateTime(2007, 2, 17)),
                new Person("Clare", "Neal", "condimentum.donec@protonmail.ca", new DateTime(2011, 10, 24)),
                new Person("Reese", "Duncan", "elit.aliquam.auctor@yahoo.couk", new DateTime(1978, 10, 2)),
                new Person("Dale", "Ferguson", "tincidunt@yahoo.ca", new DateTime(1982, 6, 17)),
                new Person("Tobias", "Oneal", "montes.nascetur.ridiculus@aol.ca", new DateTime(1991, 11, 7)),
                new Person("Tad", "Bowman", "est.mauris@yahoo.ca", new DateTime(2015, 3, 23))
            ];
        }
}