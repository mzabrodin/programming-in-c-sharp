using Lab4.PersonList.Models;
using Lab4.PersonList.Navigation;

namespace Lab4.PersonList.ViewModels;

internal class MainWindowViewModel : NavigationViewModelBase<MainNavigationType>,
    INavigatable<ApplicationNavigationType>
{
    private bool _isEnabled = true;

    public bool IsEnabled
    {
        get => _isEnabled;
        set => SetField(ref _isEnabled, value);
    }

    public ApplicationNavigationType ViewModelType => ApplicationNavigationType.Main;

    public MainWindowViewModel()
    {
        NavigateTo(MainNavigationType.PersonList);
    }

    protected override INavigatable<MainNavigationType>? CreateViewModel(MainNavigationType type)
    {
        switch (type)
        {
            case MainNavigationType.PersonList:
                return new PersonListViewModel(() => NavigateTo(MainNavigationType.PersonCreate),
                    person => NavigateTo(MainNavigationType.PersonEdit, person));
            case MainNavigationType.PersonCreate:
                return new PersonCreateViewModel(() => NavigateTo(MainNavigationType.PersonList));
            default:
                return null;
        }
    }

    protected override INavigatable<MainNavigationType>? CreateViewModel<TArgument>(MainNavigationType type,
        TArgument argument)
    {
        switch (type)
        {
            case MainNavigationType.PersonEdit:
                return new PersonEditViewModel(() => NavigateTo(MainNavigationType.PersonList), (argument as string)!);
            default:
                return null;
        }
    }
}