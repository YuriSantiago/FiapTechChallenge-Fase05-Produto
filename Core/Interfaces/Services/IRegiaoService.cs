using Core.DTOs;
using Core.Requests.Create;
using Core.Requests.Update;

namespace Core.Interfaces.Services
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
