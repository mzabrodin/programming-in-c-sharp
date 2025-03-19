using System.Windows;
using CommunityToolkit.Mvvm.Input;
using Lab2.PersonBirthdayApplication.Extensions;
using Lab2.PersonBirthdayApplication.Models;

namespace Lab2.PersonBirthdayApplication.ViewModels;

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
        await Task.Delay(2000);
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
        if (!Email.IsEmail())
        {
            MessageBox.Show("Please enter a valid email address", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            return false;
        }

        int validateAge = Birthday!.Value.IsValidAge();
        if (validateAge != 0)
        {
            MessageBox.Show(validateAge == 1
                    ? "Birthday date cannot be in the future"
                    : "Birthday date cannot be more than 135 years ago",
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            return false;
        }

        return true;
    }
}