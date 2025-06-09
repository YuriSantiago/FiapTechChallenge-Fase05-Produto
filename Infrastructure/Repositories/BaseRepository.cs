using Core.Entities;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : EntityBase
    {

        protected ApplicationDbContext _context;
        protected DbSet<T> _dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Create(T entidade)
        {
            entidade.DataInclusao = DateTime.Now;
            _dbSet.Add(entidade);
            _context.SaveChanges();
        }

        public void Update(T entidade)
        {
            _dbSet.Update(entidade);
            _context.SaveChanges();
        }

        public IList<T> GetAll()
        {
            return [.. _dbSet];
        }

        public IList<T> GetAll(Func<IQueryable<T>, IQueryable<T>>? include)
        {
            IQueryable<T> query = _dbSet;

            if (include is not null)
                query = include(query);

            return [.. query];
        }

        public T GetById(int id)
        {
            var entidade = _dbSet.FirstOrDefault(e => e.Id == id);
            return entidade is null ? throw new Exception($"Entidade com o ID {id} não encontrada.") : entidade;
        }

        public T GetById(int id, Func<IQueryable<T>, IQueryable<T>> include)
        {
            IQueryable<T> query = _dbSet;

            if (include != null)
                query = include(query);

            return query.FirstOrDefault(e => e.Id == id)
                ?? throw new KeyNotFoundException($"Entidade com o ID {id} não encontrada.");
        }

        public void Delete(int id)
        {
            _dbSet.Remove(GetById(id));
            _context.SaveChanges();
        }
    }
}
