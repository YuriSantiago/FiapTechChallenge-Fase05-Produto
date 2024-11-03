using FiapTechChallenge.Core.Entities;

namespace FiapTechChallenge.Core.Interfaces.Repositories
{
    public interface IRegiaoRepository : IRepository<Regiao>
    {
        Regiao? GetByDDD(short DDD);
    }
}
