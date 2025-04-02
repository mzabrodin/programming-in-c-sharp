using Lab4.PersonList.ViewModels;

namespace Lab4.PersonList.Navigation;

public abstract class NavigationViewModelBase<TEnum> : ViewModelBase
    where TEnum : Enum
{
    private INavigatable<TEnum>? _currentViewModel;

    public INavigatable<TEnum>? CurrentViewModel
    {
        get => _currentViewModel;
        private set
        {
            if (_currentViewModel == value)
                return;

            SetField(ref _currentViewModel, value);
        }
    }

    public void NavigateTo(TEnum type)
    {
        if (CurrentViewModel is not null && CurrentViewModel.ViewModelType.Equals(type))
            return;

        INavigatable<TEnum>? viewModel = GetViewModel(type);

        if (viewModel is null)
            return;

        CurrentViewModel = viewModel;
    }
    
    public void NavigateTo<TArgument>(TEnum type, TArgument argument)
    {
        if (CurrentViewModel is not null && CurrentViewModel.ViewModelType.Equals(type))
            return;

        INavigatable<TEnum>? viewModel = GetViewModel(type, argument);

        if (viewModel is null)
            return;

        CurrentViewModel = viewModel;
    }

    private INavigatable<TEnum>? GetViewModel(TEnum type)
    {
        return CreateViewModel(type);
    }
    
    private INavigatable<TEnum>? GetViewModel<TArgument>(TEnum type, TArgument argument)
    {
        return CreateViewModel(type, argument);
    }

    protected abstract INavigatable<TEnum>? CreateViewModel(TEnum type);
    protected abstract INavigatable<TEnum>? CreateViewModel<TArgument>(TEnum type, TArgument argument);
}