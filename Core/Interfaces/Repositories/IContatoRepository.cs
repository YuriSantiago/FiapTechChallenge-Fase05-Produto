using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IContatoRepository : IRepository<Contato>
    {
        IList<Contato> GetAllByDDD(short DDD);
    }
}
