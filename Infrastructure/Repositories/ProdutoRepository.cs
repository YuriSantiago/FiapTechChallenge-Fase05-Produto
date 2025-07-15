using Core.Entities;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {

        public ProdutoRepository(ApplicationDbContext context) : base(context)
        {

        }

        public IList<Produto> GetAllByCategory(short categoria)
        {
            return [.. _context.Produtos.Where(p => p.CategoriaId == categoria).Include(p => p.Categoria)];
        }

    }
}
