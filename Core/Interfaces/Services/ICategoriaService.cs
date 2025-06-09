using Core.DTOs;
using Core.Requests.Create;
using Core.Requests.Update;

namespace Core.Interfaces.Services
{
    public interface ICategoriaService
    {
        CategoriaDTO GetById(int id);

    }
}
