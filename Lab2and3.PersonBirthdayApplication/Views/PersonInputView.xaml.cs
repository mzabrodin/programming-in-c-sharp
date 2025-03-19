using System.Windows.Controls;
using Lab2and3.PersonBirthdayApplication.ViewModels;

namespace Lab2and3.PersonBirthdayApplication.Views;

/// <summary>
///     Interaction logic for PersonInputView.xaml
/// </summary>
public partial class PersonInputView : UserControl
{
    public PersonInputView()
    {
        InitializeComponent();
        DataContext = new PersonInputViewModel();
    }
}