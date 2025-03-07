namespace PpeManager.Domain.Seedwork;

public abstract class Enumeration : IComparable
{
    public string Name { get; private set; }

    public int Id { get; private set; }

    protected Enumeration(int id, string name) => (Id, Name) = (id, name);

    public override string ToString() => Name;

    public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
        typeof(T).GetFields(BindingFlags.Public |
                            BindingFlags.Static |
                            BindingFlags.DeclaredOnly)
                    .Select(f => f.GetValue(null))
                    .Cast<T>();

#pragma warning disable CS8765 // A nulidade do tipo de par�metro 'obj' n�o corresponde ao membro substitu�do (possivelmente devido a atributos de nulidade).
    public override bool Equals(object obj)
#pragma warning restore CS8765 // A nulidade do tipo de par�metro 'obj' n�o corresponde ao membro substitu�do (possivelmente devido a atributos de nulidade).
    {
        if (obj is not Enumeration otherValue)
        {
            return false;
        }

        var typeMatches = GetType().Equals(obj.GetType());
        var valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }

    public override int GetHashCode() => Id.GetHashCode();

    public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
    {
        var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
        return absoluteDifference;
    }

    public static T FromValue<T>(int value) where T : Enumeration
    {
        var matchingItem = Parse<T, int>(value, "value", item => item.Id == value);
        return matchingItem;
    }

    public static T FromDisplayName<T>(string displayName) where T : Enumeration
    {
        var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);
        return matchingItem;
    }

    private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
    {
        var matchingItem = GetAll<T>().FirstOrDefault(predicate);

        if (matchingItem == null)
            throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

        return matchingItem;
    }

#pragma warning disable CS8767 // A nulidade de tipos de refer�ncia no tipo de par�metro 'other' de 'int Enumeration.CompareTo(object other)' n�o corresponde ao membro implementado implicitamente 'int IComparable.CompareTo(object? obj)' (possivelmente devido a atributos de nulidade).
    public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);
#pragma warning restore CS8767 // A nulidade de tipos de refer�ncia no tipo de par�metro 'other' de 'int Enumeration.CompareTo(object other)' n�o corresponde ao membro implementado implicitamente 'int IComparable.CompareTo(object? obj)' (possivelmente devido a atributos de nulidade).
}
