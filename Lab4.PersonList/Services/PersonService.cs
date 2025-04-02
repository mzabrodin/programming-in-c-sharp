using Lab4.PersonList.Models;
using Lab4.PersonList.Repositories;
using System.Collections.Generic;
using Lab4.PersonList.Exceptions;

namespace Lab4.PersonList.Services
{
    public class PersonService
    {
        public void CreatePerson(Person person)
        {
            if (person == null)
            {
                throw new PersonNullException();
            }

            if (GetPerson(person.Email) != null)
            {
                throw new PersonAlreadyExistsException();
            }

            PersonRepository.Instance.AddPerson(person);
        }

        public bool DeletePerson(string email)
        {
            Person? person = GetPerson(email);
            if (person == null)
            {
                throw new PersonNotFoundException();
            }

            PersonRepository.Instance.RemovePerson(person);
            return true;
        }

        public void UpdatePerson(string email, Person newPerson)
        {
            if (newPerson == null)
            {
                throw new PersonNullException();
            }

            Person? person = GetPerson(email);
            if (person == null)
            {
                throw new PersonNotFoundException();
            }

            if (GetPerson(newPerson.Email) != null && newPerson.Email != email)
            {
                throw new PersonAlreadyExistsException();
            }

            PersonRepository.Instance.UpdatePerson(email, newPerson);
        }

        public Person? GetPerson(string email)
        {
            return PersonRepository.Instance.GetPersonByEmail(email);
        }

        public List<Person> GetAllPersons()
        {
            return PersonRepository.Instance.GetAllPersons();
        }
    }
}