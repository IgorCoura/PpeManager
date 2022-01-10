namespace PpeManager.Domain.Seedwork;

public abstract class ValueObject : Notifiable<Notification>
{
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
        {
            return false;
        }
#pragma warning disable CS8604 // Possível argumento de referência nula para o parâmetro 'obj' em 'bool ValueObject.Equals(object obj)'.
        return ReferenceEquals(left, null) || left.Equals(right);
#pragma warning restore CS8604 // Possível argumento de referência nula para o parâmetro 'obj' em 'bool ValueObject.Equals(object obj)'.
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !(EqualOperator(left, right));
    }

    protected abstract IEnumerable<object> GetEqualityComponents();

#pragma warning disable CS8765 // A nulidade do tipo de parâmetro 'obj' não corresponde ao membro substituído (possivelmente devido a atributos de nulidade).
    public override bool Equals(object obj)
#pragma warning restore CS8765 // A nulidade do tipo de parâmetro 'obj' não corresponde ao membro substituído (possivelmente devido a atributos de nulidade).
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;

        return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }

    public ValueObject GetCopy()
    {
#pragma warning disable CS8603 // Possível retorno de referência nula.
        return this.MemberwiseClone() as ValueObject;
#pragma warning restore CS8603 // Possível retorno de referência nula.
    }
}
