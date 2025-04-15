using Core.Entities;
using Core.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class ContatoRepository : BaseRepository<Contato>, IContatoRepository
    {

        public ContatoRepository(ApplicationDbContext context) : base(context)
        {
            
        }

    }
}
