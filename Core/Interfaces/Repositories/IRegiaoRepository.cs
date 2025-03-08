using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IRegiaoRepository : IRepository<Regiao>
    {
        Regiao? GetByDDD(short DDD);
    }
}
