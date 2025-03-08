using Core.DTOs;
using Core.Requests.Create;
using Core.Requests.Update;

namespace Core.Interfaces.Services
{
    public interface IContatoService
    {
        IList<ContatoDTO> GetAll();

        ContatoDTO GetById(int id);

        IList<ContatoDTO> GetAllByDDD(short DDD);

        void Create(ContatoRequest regiaoRequest);

        void Put(ContatoUpdateRequest regiaoUpdateRequest);

        void Delete(int id);
    }
}
