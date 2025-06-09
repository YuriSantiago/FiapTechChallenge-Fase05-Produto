using Core.DTOs;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Core.Services
{
    public class CategoriaService : ICategoriaService
    {

        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public CategoriaDTO GetById(int id)
        {
            var categoria = _categoriaRepository.GetById(id);

            var categoriaDTO = new CategoriaDTO()
            {
                Id = categoria.Id,
                DataInclusao = categoria.DataInclusao,
                Descricao = categoria.Descricao
            };

            return categoriaDTO;
        }

    }
}
