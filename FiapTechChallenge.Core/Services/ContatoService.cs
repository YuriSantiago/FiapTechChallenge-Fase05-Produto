using FiapTechChallenge.Core.DTOs;
using FiapTechChallenge.Core.Entities;
using FiapTechChallenge.Core.Interfaces.Repositories;
using FiapTechChallenge.Core.Interfaces.Services;
using FiapTechChallenge.Core.Requests.Create;
using FiapTechChallenge.Core.Requests.Update;
using Microsoft.EntityFrameworkCore;

namespace FiapTechChallenge.Core.Services
{
    public class ContatoService : IContatoService
    {

        private readonly IContatoRepository _contatoRepository;
        private readonly IRegiaoRepository _regiaoRepository;

        public ContatoService(IContatoRepository contatoRepository, IRegiaoRepository regiaoRepository)
        {
            _contatoRepository = contatoRepository;
            _regiaoRepository = regiaoRepository;
        }

        public IList<ContatoDTO> GetAll()
        {
            var contatosDTO = new List<ContatoDTO>();
            var contatos = _contatoRepository.GetAll(c => c.Include(r => r.Regiao));

            foreach (var contato in contatos)
            {
                contatosDTO.Add(new ContatoDTO()
                {
                    Id = contato.Id,
                    DataInclusao = contato.DataInclusao,
                    Nome = contato.Nome,
                    Telefone = contato.Telefone,
                    Email = contato.Email,
                    Regiao = new RegiaoDTO()
                    {
                        Id = contato.Regiao.Id,
                        DataInclusao = contato.Regiao.DataInclusao,
                        DDD = contato.Regiao.DDD,
                        Descricao = contato.Regiao.Descricao
                    }
                });
            }

            return contatosDTO;

        }

        public ContatoDTO GetById(int id)
        {
            var contato = _contatoRepository.GetById(id, c => c.Include(r => r.Regiao));

            var contatoDTO = new ContatoDTO()
            {
                Id = contato.Id,
                DataInclusao = contato.DataInclusao,
                Nome = contato.Nome,
                Telefone = contato.Telefone,
                Email = contato.Email,
                Regiao = new RegiaoDTO()
                {
                    Id = contato.Regiao.Id,
                    DataInclusao = contato.Regiao.DataInclusao,
                    DDD = contato.Regiao.DDD,
                    Descricao = contato.Regiao.Descricao
                }
            };

            return contatoDTO;
        }

        public IList<ContatoDTO> GetAllByDDD(short ddd)
        {
            var contatosDTO = new List<ContatoDTO>();
            var contatos = _contatoRepository.GetAllByDDD(ddd);

            foreach (var contato in contatos)
            {
                contatosDTO.Add(new ContatoDTO()
                {
                    Id = contato.Id,
                    DataInclusao = contato.DataInclusao,
                    Nome = contato.Nome,
                    Telefone = contato.Telefone,
                    Email = contato.Email,
                    Regiao = new RegiaoDTO()
                    {
                        Id = contato.Regiao.Id,
                        DataInclusao = contato.Regiao.DataInclusao,
                        DDD = contato.Regiao.DDD,
                        Descricao = contato.Regiao.Descricao
                    }
                });
            }

            return contatosDTO;
        }

        public void Create(ContatoRequest contatoRequest)
        {
            var regiao = _regiaoRepository.GetByDDD(contatoRequest.DDD);

            if (regiao is not null)
            {
                var contato = new Contato()
                {
                    Nome = contatoRequest.Nome,
                    Telefone = contatoRequest.Telefone,
                    Email = contatoRequest.Email,
                    RegiaoId = regiao.Id,
                    Regiao = regiao
                };

                _contatoRepository.Create(contato);
            }
        }

        public void Put(ContatoUpdateRequest contatoUpdateRequest)
        {
            var contato = _contatoRepository.GetById(contatoUpdateRequest.Id);

            contato.Nome = contatoUpdateRequest.Nome ?? contato.Nome;
            contato.Telefone = contatoUpdateRequest.Telefone ?? contato.Telefone;
            contato.Email = contatoUpdateRequest.Email ?? contato.Email;

            if (contatoUpdateRequest.DDD is not null)
            {
                var regiao = _regiaoRepository.GetByDDD(contatoUpdateRequest.DDD.Value);

                if (regiao is not null)
                    contato.RegiaoId = regiao.Id;
            }

            _contatoRepository.Update(contato);
        }

        public void Delete(int id)
        {
            _contatoRepository.Delete(id);
        }


    }
}
