using System.Windows;

namespace Lab4.PersonList.Extensions;

public static class MessageBoxTypes
{
    public static void MessageBoxErrorShow(string message)
    {
        MessageBox.Show(message, "Error", MessageBoxButton.OK,
            MessageBoxImage.Error);
    }
    
    public static void MessageBoxSuccessShow(string message)
    {
        MessageBox.Show(message, "Success", MessageBoxButton.OK,
            MessageBoxImage.Information);
    }
}