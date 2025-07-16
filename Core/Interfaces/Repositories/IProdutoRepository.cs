using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {

        IList<Produto> GetAllByCategory(int categoriaId);

    }
}
