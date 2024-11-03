using FiapTechChallenge.Core.Entities;

namespace FiapTechChallenge.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : EntityBase

    {
        IList<T> GetAll();

        IList<T> GetAll(Func<IQueryable<T>, IQueryable<T>>? include);

        T GetById(int id);

        T GetById(int id, Func<IQueryable<T>, IQueryable<T>> include);

        void Create(T entidade);

        void Update(T entidade);

        void Delete(int id);

    }
}
