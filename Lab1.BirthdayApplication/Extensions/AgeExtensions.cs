using System.Windows;

namespace Lab1.BirthdayApplication.Extensions;

public static class AgeExtensions
{
    public static int GetAge(this DateTime date)
    {
        DateTime today = DateTime.Today;
        int age = today.Year - date.Year;
        if (date.Date > today.AddYears(-age))
        {
            age--;
        }
        
        return age;
    }

    public static bool IsBirthday(this DateTime date)
    {
        return date.Month == DateTime.Today.Month && date.Day == DateTime.Today.Day;
    }
    
    public static bool ValidateAge(this DateTime date)
    {
        if (date > DateTime.Today)
        {
            MessageBox.Show("Are you from the future?", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }
        
        if (date.GetAge() > 135)
        {
            MessageBox.Show("Bro, you too old", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }
        
        return true;
    }
}