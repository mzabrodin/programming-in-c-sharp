using System.Windows.Controls;
using Lab1.BirthdayApplication.ViewModels;

namespace Lab1.BirthdayApplication.Views;

/// <summary>
///     Interaction logic for BirthdayFormView.xaml
/// </summary>
public partial class BirthdayFormView : UserControl
{
    public BirthdayFormView()
    {
        InitializeComponent();
        DataContext = new BirthdayFormViewModel();
    }
}