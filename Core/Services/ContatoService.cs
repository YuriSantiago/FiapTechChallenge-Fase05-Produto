using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Requests.Create;

namespace Core.Services
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


    }
}
