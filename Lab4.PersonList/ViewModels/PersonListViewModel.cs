using System.Collections.ObjectModel;
using System.Windows.Data;
using CommunityToolkit.Mvvm.Input;
using Lab4.PersonList.Exceptions;
using Lab4.PersonList.Extensions;
using Lab4.PersonList.Models;
using Lab4.PersonList.Navigation;
using Lab4.PersonList.Services;

namespace Lab4.PersonList.ViewModels
{
    public class PersonListViewModel : ViewModelBase, INavigatable<MainNavigationType>
    {
        private readonly PersonService _personService;
        private List<Person> _allPersons;
        private ObservableCollection<Person> _personList;

        private PersonListProperty _sortListProperty = PersonListProperty.None;
        private bool _isAscending;
        private PersonListProperty _filterListProperty = PersonListProperty.None;
        private string _filterText = String.Empty;
        private bool _isLoading;

        public PersonListViewModel(Action goToPersonCreate, Action<string?> goToEditUser)
        {
            _personService = new PersonService();
            LoadPersons();

            GoToPersonCreateCommand = new RelayCommand(goToPersonCreate);
            GotoEditUserCommand = new RelayCommand<string>(goToEditUser);
            RemoveUserCommand = new RelayCommand<string>(RemovePerson);
            SortListCommand = new AsyncRelayCommand(Sort, CanSort);
            FilterListCommand = new AsyncRelayCommand(Filter, CanFilter);
            ResetSortFilterCommand = new AsyncRelayCommand(Reset, CanReset);
        }

        public ObservableCollection<Person> PersonList
        {
            get => _personList;
            set => SetField(ref _personList, value);
        }

        public PersonListProperty SortListProperty
        {
            get => _sortListProperty;
            set
            {
                SetField(ref _sortListProperty, value);
                SortListCommand.NotifyCanExecuteChanged();
                ResetSortFilterCommand.NotifyCanExecuteChanged();
            }
        }

        public PersonListProperty FilterListProperty
        {
            get => _filterListProperty;
            set
            {
                SetField(ref _filterListProperty, value);
                FilterListCommand.NotifyCanExecuteChanged();
                ResetSortFilterCommand.NotifyCanExecuteChanged();
            }
        }

        public string FilterText
        {
            get => _filterText;
            set
            {
                SetField(ref _filterText, value);
                FilterListCommand.NotifyCanExecuteChanged();
                ResetSortFilterCommand.NotifyCanExecuteChanged();
            }
        }

        public bool IsAscending
        {
            get => _isAscending;
            set
            {
                SetField(ref _isAscending, value);
                SortListCommand.NotifyCanExecuteChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetField(ref _isLoading, value);
        }

        public RelayCommand GoToPersonCreateCommand { get; }
        public RelayCommand<string> GotoEditUserCommand { get; }
        public RelayCommand<string> RemoveUserCommand { get; }
        public AsyncRelayCommand SortListCommand { get; }
        public AsyncRelayCommand FilterListCommand { get; }
        public AsyncRelayCommand ResetSortFilterCommand { get; }

        private void LoadPersons()
        {
            _allPersons = _personService.GetAllPersons();
            PersonList = new ObservableCollection<Person>(_allPersons);
        }

        private async Task Sort()
        {
            IsLoading = true;
            await Task.Delay(1000);
            await Task.Run(() =>
            {
                IEnumerable<Person> sortedList = _isAscending
                    ? PersonList.OrderBy(p => GetKey(p, _sortListProperty))
                    : PersonList.OrderByDescending(p => GetKey(p, _sortListProperty));

                PersonList = new ObservableCollection<Person>(sortedList);
            });
            IsLoading = false;
        }

        private async Task Filter()
        {
            IsLoading = true;
            
            if (_filterListProperty is PersonListProperty.IsAdult or PersonListProperty.IsBirthday)
            {
                _filterText = FilterText switch
                {
                    "Yes" => "true",
                    "No" => "false",
                    _ => FilterText
                };
            }
            
            await Task.Delay(1000);
            await Task.Run(() =>
            {
                IEnumerable<Person> filteredList = PersonList.Where(p =>
                    GetKey(p, _filterListProperty).ToString()
                        ?.Contains(_filterText, StringComparison.OrdinalIgnoreCase) == true);

                PersonList = new ObservableCollection<Person>(filteredList);
            });
            IsLoading = false;
        }

        private async Task Reset()
        {
            IsLoading = true;
            await Task.Delay(1000);
            await Task.Run(() => { PersonList = new ObservableCollection<Person>(_allPersons); });

            SortListProperty = PersonListProperty.None;
            FilterListProperty = PersonListProperty.None;
            IsAscending = false;
            FilterText = String.Empty;
            IsLoading = false;
        }

        private bool CanFilter()
        {
            return _personList.Count > 0 &&
                   _filterListProperty != PersonListProperty.None &&
                   !String.IsNullOrWhiteSpace(_filterText);
        }

        private bool CanSort()
        {
            return _personList.Count > 0 &&
                   _sortListProperty != PersonListProperty.None;
        }

        private bool CanReset()
        {
            return _sortListProperty != PersonListProperty.None ||
                   _filterListProperty != PersonListProperty.None;
        }

        private object GetKey(Person person, PersonListProperty personListProperty)
        {
            return personListProperty switch
            {
                PersonListProperty.Name => person.Name,
                PersonListProperty.Surname => person.Surname,
                PersonListProperty.Email => person.Email,
                PersonListProperty.Birthday => person.Birthday,
                PersonListProperty.Age => person.Age,
                PersonListProperty.IsAdult => person.IsAdult,
                PersonListProperty.SunSign => person.SunSign,
                PersonListProperty.ChineseSign => person.ChineseSign,
                PersonListProperty.IsBirthday => person.IsBirthday,
                _ => person.Name
            };
        }

        private void RemovePerson(string? email)
        {
            var isRemoved = false;
            try
            {
                isRemoved = _personService.DeletePerson(email);
            }
            catch (PersonNotFoundException e)
            {
                MessageBoxTypes.MessageBoxErrorShow(e.Message);
            }

            if (isRemoved)
            {
                Person? personToRemove = _personList.FirstOrDefault(x => x.Email == email);
                if (personToRemove == null) return;
                _personList.Remove(personToRemove);
                MessageBoxTypes.MessageBoxSuccessShow("Person removed successfully");
            }
        }

        public MainNavigationType ViewModelType => MainNavigationType.PersonList;
    }
}