using Core.DTOs;
using Core.Requests.Create;

namespace Core.Interfaces.Services
{
    public interface IRegiaoService
    {
        RegiaoDTO? GetByDDD(short DDD);

        void Create(RegiaoRequest regiaoRequest);
    }
}
