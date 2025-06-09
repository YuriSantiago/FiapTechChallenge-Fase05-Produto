using Core.Entities;
using Core.Interfaces.Repositories;

namespace Infrastructure.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {

        public CategoriaRepository(ApplicationDbContext context) : base(context)
        {

        }

    }
}
