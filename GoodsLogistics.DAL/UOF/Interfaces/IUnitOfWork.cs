using GoodsLogistics.DAL.Repositories.Interfaces;

namespace GoodsLogistics.DAL.UOF.Interfaces
{
    public interface IUnitOfWork
    {
        void Save();

        IGenericRepository<T> GetRepository<T>() where T : class;
    }
}
