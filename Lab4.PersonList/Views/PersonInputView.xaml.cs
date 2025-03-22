using System.Windows.Controls;
using Lab4.PersonList.ViewModels;

namespace Lab4.PersonList.Views;

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