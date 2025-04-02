using System.Windows;
using CommunityToolkit.Mvvm.Input;
using Lab4.PersonList.Exceptions;
using Lab4.PersonList.Extensions;
using Lab4.PersonList.Models;
using Lab4.PersonList.Navigation;
using Lab4.PersonList.Services;

namespace Lab4.PersonList.ViewModels;

public class PersonCreateViewModel : ViewModelBase, INavigatable<MainNavigationType>
{
    private readonly Action _goToPersonList;
    private readonly PersonService _personService = new();
    private bool _isLoading;

    #region Person Fields

    private string _name = String.Empty;
    private string _surname = String.Empty;
    private string _email = String.Empty;
    private DateTime? _birthday;
    private Person? _person;

    #endregion

    public PersonCreateViewModel(Action goToPersonList)
    {
        _goToPersonList = goToPersonList;
        PersonCreateCommand = new AsyncRelayCommand(Create, CanCreate);
        BackCommand = new RelayCommand(goToPersonList);
    }

    public bool IsLoading
    {
        get => _isLoading;
        set => SetField(ref _isLoading, value);
    }

    public string Name
    {
        get => _name;
        set
        {
            SetField(ref _name, value);
            PersonCreateCommand.NotifyCanExecuteChanged();
        }
    }

    public string Surname
    {
        get => _surname;
        set
        {
            SetField(ref _surname, value);
            PersonCreateCommand.NotifyCanExecuteChanged();
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            SetField(ref _email, value);
            PersonCreateCommand.NotifyCanExecuteChanged();
        }
    }

    public DateTime? Birthday
    {
        get => _birthday;
        set
        {
            SetField(ref _birthday, value);
            PersonCreateCommand.NotifyCanExecuteChanged();
        }
    }

    public Person? Person
    {
        get => _person;
        set
        {
            SetField(ref _person, value);
            PersonCreateCommand.NotifyCanExecuteChanged();
        }
    }

    public AsyncRelayCommand PersonCreateCommand { get; }
    public RelayCommand BackCommand { get; }

    private bool CanCreate()
    {
        return !String.IsNullOrEmpty(Name) &&
               !String.IsNullOrEmpty(Surname) &&
               !String.IsNullOrEmpty(Email) &&
               Birthday.HasValue;
    }

    private async Task Create()
    {
        IsLoading = true;
        await Task.Delay(1500);
        Person? person = await Task.Run(() =>
        {
            if (!IsPersonValid()) return null;

            return new Person(Name, Surname, Email, Birthday!.Value);
        });

        Person = person;
        try
        {
            _personService.CreatePerson(person);
        }
        catch (Exception e) when (e is PersonNullException or PersonAlreadyExistsException)
        {
            MessageBoxTypes.MessageBoxErrorShow(e.Message);
            IsLoading = false;
            return;
        }

        MessageBoxTypes.MessageBoxSuccessShow("Person created successfully");

        IsLoading = false;
        _goToPersonList();
    }

    private bool IsPersonValid()
    {
        try
        {
            Name.ValidNameLength();
            Name.StartsWithCapitalLetter();
            Name.ContainsOnlyLetters();

            Surname.ValidNameLength();
            Surname.StartsWithCapitalLetter();
            Surname.ContainsOnlyLetters();

            Email.IsEmail();

            Birthday!.Value.IsValidAge();
        }
        catch (Exception e) when (e is NameFormatException or EmailFormatException or BirthdayInTheFutureException
                                      or BirthdayInThePastException)
        {
            MessageBoxTypes.MessageBoxErrorShow(e.Message);
            return false;
        }

        return true;
    }


    public MainNavigationType ViewModelType => MainNavigationType.PersonCreate;
}