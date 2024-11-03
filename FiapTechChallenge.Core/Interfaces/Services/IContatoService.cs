using FiapTechChallenge.Core.DTOs;
using FiapTechChallenge.Core.Requests.Create;
using FiapTechChallenge.Core.Requests.Update;

namespace FiapTechChallenge.Core.Interfaces.Services
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
