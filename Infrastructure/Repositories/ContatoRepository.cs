using Core.Entities;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ContatoRepository : BaseRepository<Contato>, IContatoRepository
    {

        public ContatoRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public IList<Contato> GetAllByDDD(short DDD)
        {
            return [.. _context.Contatos.Include(c => c.Regiao).Where(r => r.Regiao.DDD == DDD)];
        }

    }
}
