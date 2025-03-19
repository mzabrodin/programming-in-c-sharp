using System.Windows.Controls;
using Lab2.PersonBirthdayApplication.ViewModels;

namespace Lab2.PersonBirthdayApplication.Views;

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