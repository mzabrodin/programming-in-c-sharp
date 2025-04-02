using System.Windows;
using CommunityToolkit.Mvvm.Input;
using Lab4.PersonList.Exceptions;
using Lab4.PersonList.Extensions;
using Lab4.PersonList.Models;
using Lab4.PersonList.Navigation;
using Lab4.PersonList.Services;

namespace Lab4.PersonList.ViewModels;

public class PersonEditViewModel : ViewModelBase, INavigatable<MainNavigationType>
{
    private readonly Action _goToPersonList;
    private readonly PersonService _personService = new();
    private bool _isLoading;
    private Person _originalPerson;

    #region Person Fields

    private string _name;
    private string _surname;
    private string _email;
    private DateTime? _birthday;

    #endregion

    public PersonEditViewModel(Action goToPersonList, string email)
    {
        _goToPersonList = goToPersonList;
        _originalPerson = _personService.GetPerson(email);
        
        if (_originalPerson != null)
        {
            _name = _originalPerson.Name;
            _surname = _originalPerson.Surname;
            _email = _originalPerson.Email;
            _birthday = _originalPerson.Birthday;
        }

        SaveCommand = new AsyncRelayCommand(Save, CanSave);
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
            SaveCommand.NotifyCanExecuteChanged();
        }
    }

    public string Surname
    {
        get => _surname;
        set
        {
            SetField(ref _surname, value);
            SaveCommand.NotifyCanExecuteChanged();
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            SetField(ref _email, value);
            SaveCommand.NotifyCanExecuteChanged();
        }
    }

    public DateTime? Birthday
    {
        get => _birthday;
        set
        {
            SetField(ref _birthday, value);
            SaveCommand.NotifyCanExecuteChanged();
        }
    }

    public AsyncRelayCommand SaveCommand { get; }
    public RelayCommand BackCommand { get; }

    private bool CanSave()
    {
        return !String.IsNullOrEmpty(Name) &&
               !String.IsNullOrEmpty(Surname) &&
               !String.IsNullOrEmpty(Email) &&
               Birthday.HasValue;
    }

    private async Task Save()
    {
        IsLoading = true;
        await Task.Delay(1500);
        Person? person = await Task.Run(() =>
        {
            if (!IsPersonValid()) return null;

            return new Person(Name, Surname, Email, Birthday!.Value);
        });

        try
        {
            _personService.UpdatePerson(_originalPerson.Email, person);
        }
        catch (Exception e) when (e is PersonNullException or PersonNotFoundException or PersonAlreadyExistsException)
        {
            MessageBoxTypes.MessageBoxErrorShow(e.Message);
            IsLoading = false;
            return;
        }

        MessageBoxTypes.MessageBoxSuccessShow("Person changed successfully");

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

    public MainNavigationType ViewModelType => MainNavigationType.PersonEdit;
}