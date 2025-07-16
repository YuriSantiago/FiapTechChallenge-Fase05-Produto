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

        public IList<Produto> GetAllByCategory(int categoriaId)
        {
            return [.. _context.Produtos.Where(p => p.CategoriaId == categoriaId).Include(p => p.Categoria)];
        }

    }
}
