using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Lab1.BirthdayApplication.Extensions;
using Lab1.BirthdayApplication.Models;

namespace Lab1.BirthdayApplication.ViewModels;

public class BirthdayFormViewModel : INotifyPropertyChanged
{
    private DateTime? _birthday;
    private int? _age;
    private WesternZodiac? _westernZodiac;
    private ChineseZodiac? _chineseZodiac;
    private bool? _isBirthday;
    public event PropertyChangedEventHandler? PropertyChanged;
    
    public DateTime? Birthday
    {
        get => _birthday;
        set
        {
            SetField(ref _birthday, value);
            if (value == null)
            {
                Age = null;
                WesternZodiac = null;
                ChineseZodiac = null;
                IsBirthday = null;
                return;
            }

            if (!value.Value.ValidateAge())
            {
                Birthday = null;
                return;
            }
            
            Age = value.Value.GetAge();
            WesternZodiac = value.Value.GetWesternZodiac();
            ChineseZodiac = value.Value.GetChineseZodiac();
            IsBirthday = value.Value.IsBirthday();
        }
    }

    public int? Age
    {
        get => _age;
        set => SetField(ref _age, value);
    }

    public WesternZodiac? WesternZodiac
    {
        get => _westernZodiac;
        set => SetField(ref _westernZodiac, value);
    }

    public ChineseZodiac? ChineseZodiac
    {
        get => _chineseZodiac;
        set => SetField(ref _chineseZodiac, value);
    }

    public bool? IsBirthday
    {
        get => _isBirthday;
        set => SetField(ref _isBirthday, value);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}