using FiapTechChallenge.Core.Entities;

namespace FiapTechChallenge.Core.Interfaces.Repositories
{
    public interface IContatoRepository : IRepository<Contato>
    {
        IList<Contato> GetAllByDDD(short DDD);
    }
}
