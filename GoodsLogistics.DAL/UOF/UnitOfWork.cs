using GoodsLogistics.DAL.EF;
using GoodsLogistics.DAL.Factories.Interfaces;
using GoodsLogistics.DAL.Repositories.Interfaces;
using GoodsLogistics.DAL.UOF.Interfaces;

namespace GoodsLogistics.DAL.UOF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GoodsLogisticsContext _db;
        private readonly IRepositoryFactory _repositoryFactory;

        public UnitOfWork(
            GoodsLogisticsContext db, 
            IRepositoryFactory repositoryFactory)
        {
            _db = db;
            _repositoryFactory = repositoryFactory;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return _repositoryFactory.GetRepositoryInstance<T>();
        }
    }
}
