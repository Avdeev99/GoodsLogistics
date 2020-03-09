using GoodsLogistics.DAL.Repositories.Interfaces;

namespace GoodsLogistics.DAL.Factories.Interfaces
{
    public interface IRepositoryFactory
    {
        IGenericRepository<T> GetRepositoryInstance<T>() where T : class;
    }
}
