namespace PpeManager.Domain.Seedwork;

public abstract class Entity : Notifiable<Notification>
{
    int? _requestedHashCode;
    int _Id;
    public virtual int Id
    {
        get
        {
            return _Id;
        }
        protected set
        {
            _Id = value;
        }
    }

#pragma warning disable CS8618 // O campo não anulável '_domainEvents' precisa conter um valor não nulo ao sair do construtor. Considere declarar o campo como anulável.
    private List<INotification> _domainEvents;
#pragma warning restore CS8618 // O campo não anulável '_domainEvents' precisa conter um valor não nulo ao sair do construtor. Considere declarar o campo como anulável.
#pragma warning disable CS8603 // Possível retorno de referência nula.
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();
#pragma warning restore CS8603 // Possível retorno de referência nula.

    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents = _domainEvents ?? new List<INotification>();
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(INotification eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    public bool IsTransient()
    {
        return this.Id == default(Int32);
    }

#pragma warning disable CS8765 // A nulidade do tipo de parâmetro 'obj' não corresponde ao membro substituído (possivelmente devido a atributos de nulidade).
    public override bool Equals(object obj)
#pragma warning restore CS8765 // A nulidade do tipo de parâmetro 'obj' não corresponde ao membro substituído (possivelmente devido a atributos de nulidade).
    {
        if (obj == null || obj is not Entity)
            return false;

        if (Object.ReferenceEquals(this, obj))
            return true;

        if (this.GetType() != obj.GetType())
            return false;

        Entity item = (Entity)obj;

        if (item.IsTransient() || this.IsTransient())
            return false;
        else
            return item.Id == this.Id;
    }

    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            if (!_requestedHashCode.HasValue)
                _requestedHashCode = this.Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

            return _requestedHashCode.Value;
        }
        else
            return base.GetHashCode();

    }
    public static bool operator ==(Entity left, Entity right)
    {
        if (Object.Equals(left, null))
            return (Object.Equals(right, null)) ? true : false;
        else
            return left.Equals(right);
    }

    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right);
    }
}
