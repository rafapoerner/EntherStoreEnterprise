namespace ESE.Core.Data
{
    // Regra: Apenas um repositório por agregação.
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
