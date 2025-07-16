using Core.DTOs;
using Core.Requests.Create;
using Core.Requests.Update;

namespace Core.Interfaces.Services
{
    public interface IProdutoService
    {

        IList<ProdutoDTO> GetAll();

        ProdutoDTO GetById(int id);

        IList<ProdutoDTO> GetAllByCategory(int categoriaId);

        void Create(ProdutoRequest produtoRequest);

        void Put(ProdutoUpdateRequest produtoUpdateRequest);

        void Delete(int id);

    }
}
