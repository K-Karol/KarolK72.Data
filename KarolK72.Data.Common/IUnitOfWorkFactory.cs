namespace KarolK72.Data.Common
{
    public interface IUnitOfWorkFactory<T> where T : IDisposable
    {
        IUnitOfWork<T> CreateNew();
    }
}