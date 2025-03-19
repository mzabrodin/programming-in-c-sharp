using System.Windows;
using CommunityToolkit.Mvvm.Input;
using Lab2and3.PersonBirthdayApplication.Extensions;
using Lab2and3.PersonBirthdayApplication.Exceptions;
using Lab2and3.PersonBirthdayApplication.Models;

namespace Lab2and3.PersonBirthdayApplication.ViewModels;

public class PersonInputViewModel : ViewModelBase
{
    private bool _isLoading;
    private string _name = String.Empty;
    private string _surname = String.Empty;
    private string _email = String.Empty;
    private DateTime? _birthday;
    private Person? _person;

    public PersonInputViewModel()
    {
        ProceedCommand = new AsyncRelayCommand(Proceed, CanExecute);
#if DEBUG
        Name = "John";
        Surname = "Doe";
        Email = "johndoe@gmail.com";
        Birthday = new DateTime(2000, 1, 1);
#endif
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
            ProceedCommand.NotifyCanExecuteChanged();
        }
    }

    public string Surname
    {
        get => _surname;
        set
        {
            SetField(ref _surname, value);
            ProceedCommand.NotifyCanExecuteChanged();
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            SetField(ref _email, value);
            ProceedCommand.NotifyCanExecuteChanged();
        }
    }

    public DateTime? Birthday
    {
        get => _birthday;
        set
        {
            SetField(ref _birthday, value);
            ProceedCommand.NotifyCanExecuteChanged();
        }
    }

    public Person? Person
    {
        get => _person;
        set
        {
            SetField(ref _person, value);
            ProceedCommand.NotifyCanExecuteChanged();
        }
    }

    public AsyncRelayCommand ProceedCommand { get; }

    private bool CanExecute()
    {
        return !String.IsNullOrEmpty(Name) &&
               !String.IsNullOrEmpty(Surname) &&
               !String.IsNullOrEmpty(Email) &&
               Birthday.HasValue;
    }

    private async Task Proceed()
    {
        IsLoading = true;
        Person = null;
        await Task.Delay(1500);
        Person? person = await Task.Run(() =>
        {
            if (!IsPersonValid()) return null;

            return new Person(Name, Surname, Email, Birthday!.Value);
        });

        Person = person;
        IsLoading = false;
    }

    private bool IsPersonValid()
    {
        #region NameValidation
        try
        {
            Name.ValidNameLength();
            Name.StartsWithCapitalLetter();
            Name.ContainsOnlyLetters();
        }
        catch (NameFormatException e)
        {
            MessageBoxErrorShow(e.Message);
        }
        #endregion
        
        #region SurnameValidation
        try
        {
            Surname.ValidNameLength();
            Surname.StartsWithCapitalLetter();
            Surname.ContainsOnlyLetters();
        }
        catch (NameFormatException e)
        {
            MessageBoxErrorShow(e.Message);
        }
        #endregion
        
        #region EmailValidation
        try
        {
            Email!.IsEmail();
        }
        catch (EmailFormatException e)
        {
            MessageBoxErrorShow(e.Message);
            return false;
        }
        #endregion

        #region BirthdayValidation
        try
        {
            Birthday!.Value.IsValidAge();
        }
        catch (BirthdayInTheFutureException e)
        {
            MessageBoxErrorShow(e.Message);
            return false;
        }
        catch (BirthdayInThePastException e)
        {
            MessageBoxErrorShow(e.Message);
            return false;
        }
        #endregion

        return true;
    }

    private static void MessageBoxErrorShow(string message)
    {
        MessageBox.Show(message, "Error", MessageBoxButton.OK,
            MessageBoxImage.Error);
    }
}