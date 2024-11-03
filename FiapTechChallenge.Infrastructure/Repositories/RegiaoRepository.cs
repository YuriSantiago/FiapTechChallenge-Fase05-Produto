using FiapTechChallenge.Core.Entities;
using FiapTechChallenge.Core.Interfaces.Repositories;

namespace FiapTechChallenge.Infrastructure.Repositories
{
    public class RegiaoRepository : BaseRepository<Regiao>, IRegiaoRepository
    {

        public RegiaoRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public Regiao? GetByDDD(short DDD)
        {
            return _context.Regioes.FirstOrDefault(r => r.DDD == DDD);
        }
    }
}
