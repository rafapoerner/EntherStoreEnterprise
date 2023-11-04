namespace ESE.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
