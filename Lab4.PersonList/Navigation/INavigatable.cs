namespace Lab4.PersonList.Navigation;

public interface INavigatable<TEnum> where TEnum : Enum
{
    public TEnum ViewModelType { get; }
}