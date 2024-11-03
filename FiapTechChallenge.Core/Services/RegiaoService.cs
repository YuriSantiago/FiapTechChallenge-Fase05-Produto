using FiapTechChallenge.Core.DTOs;
using FiapTechChallenge.Core.Entities;
using FiapTechChallenge.Core.Interfaces.Repositories;
using FiapTechChallenge.Core.Interfaces.Services;
using FiapTechChallenge.Core.Requests.Create;
using FiapTechChallenge.Core.Requests.Update;

namespace FiapTechChallenge.Core.Services
{
    public class RegiaoService : IRegiaoService
    {

        private readonly IRegiaoRepository _regiaoRepository;

        public RegiaoService(IRegiaoRepository regiaoRepository)
        {
            _regiaoRepository = regiaoRepository;
        }

        public IList<RegiaoDTO> GetAll()
        {
            var regioesDTO = new List<RegiaoDTO>();
            var regioes = _regiaoRepository.GetAll();

            foreach (var regiao in regioes)
            {
                regioesDTO.Add(new RegiaoDTO()
                {
                    Id = regiao.Id,
                    DataInclusao = regiao.DataInclusao,
                    DDD = regiao.DDD,
                    Descricao = regiao.Descricao
                });
            }

            return regioesDTO;

        }

        public RegiaoDTO GetById(int id)
        {
            var regiao = _regiaoRepository.GetById(id);

            var regiaoDTO = new RegiaoDTO()
            {
                Id = regiao.Id,
                DataInclusao = regiao.DataInclusao,
                DDD = regiao.DDD,
                Descricao = regiao.Descricao
            };

            return regiaoDTO;
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

            public void Put(RegiaoUpdateRequest regiaoUpdateRequest)
            {
                var regiao = _regiaoRepository.GetById(regiaoUpdateRequest.Id);
                regiao.DDD = regiaoUpdateRequest.DDD ?? regiao.DDD;
                regiao.Descricao = regiaoUpdateRequest.Descricao ?? regiao.Descricao;

                _regiaoRepository.Update(regiao);
            }

            public void Delete(int id)
            {
                _regiaoRepository.Delete(id);
            }


        }
    }
