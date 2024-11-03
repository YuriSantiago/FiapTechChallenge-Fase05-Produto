using FiapTechChallenge.Core.DTOs;
using FiapTechChallenge.Core.Requests.Create;
using FiapTechChallenge.Core.Requests.Update;

namespace FiapTechChallenge.Core.Interfaces.Services
{
    public interface IRegiaoService
    {
        IList<RegiaoDTO> GetAll();

        RegiaoDTO GetById(int id);

        RegiaoDTO? GetByDDD(short DDD);

        void Create(RegiaoRequest regiaoRequest);

        void Put(RegiaoUpdateRequest regiaoUpdateRequest);

        void Delete(int id);
    }
}
