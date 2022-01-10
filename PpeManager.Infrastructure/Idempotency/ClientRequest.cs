namespace Microsoft.eShopOnContainers.Services.Ordering.Infrastructure.Idempotency;

public class ClientRequest
{
    public Guid Id { get; set; }
#pragma warning disable CS8618 // O propriedade não anulável 'Name' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
    public string Name { get; set; }
#pragma warning restore CS8618 // O propriedade não anulável 'Name' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
    public DateTime Time { get; set; }
}
