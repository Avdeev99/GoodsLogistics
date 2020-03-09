using System;
using GoodsLogistics.DAL.Factories.Interfaces;
using GoodsLogistics.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GoodsLogistics.DAL.Factories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public RepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IGenericRepository<T> GetRepositoryInstance<T>() where T : class
        {
            return _serviceProvider.GetRequiredService<IGenericRepository<T>>();
        }
    }
}
