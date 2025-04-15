using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Requests.Create;

namespace Core.Services
{
    public class RegiaoService : IRegiaoService
    {

        private readonly IRegiaoRepository _regiaoRepository;

        public RegiaoService(IRegiaoRepository regiaoRepository)
        {
            _regiaoRepository = regiaoRepository;
        }

        public RegiaoDTO? GetByDDD(short ddd)
        {
            var regiao = _regiaoRepository.GetByDDD(ddd);

            if (regiao is not null)
            {
                var regiaoDTO = new RegiaoDTO()
                {
                    Id = regiao.Id,
                    DataInclusao = regiao.DataInclusao,
                    DDD = regiao.DDD,
                    Descricao = regiao.Descricao
                };

                return regiaoDTO;
            }

            return null;
        }

        public void Create(RegiaoRequest regiaoRequest)
        {
            var regiao = new Regiao()
            {
                DDD = regiaoRequest.DDD,
                Descricao = regiaoRequest.Descricao
            };

            _regiaoRepository.Create(regiao);
        }

    }
}
