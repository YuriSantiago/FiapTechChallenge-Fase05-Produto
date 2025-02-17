using FiapTechChallenge.Core.Interfaces.Services;
using FiapTechChallenge.Core.Requests.Create;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ContatoController : ControllerBase
    {

        private readonly IContatoService _contatoService;
        private readonly IRegiaoService _regiaoService;

        public ContatoController(IContatoService contatoService, IRegiaoService regiaoService)
        {
            _contatoService = contatoService;
            _regiaoService = regiaoService;
        }


        /// <summary>
        /// Cadastra um novo contato 
        /// </summary>
        /// <param name="contatoRequest">Objeto do tipo "ContatoRequest"</param>
        /// <response code="200">Contato cadastrado com sucesso</response>
        /// <response code="400">Erro ao cadastrar o contato</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost]
        public IActionResult Post([FromBody] ContatoRequest contatoRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var regiao = _regiaoService.GetByDDD(contatoRequest.DDD);

                if (regiao is null)
                    return NotFound("DDD não encontrado");

                _contatoService.Create(contatoRequest);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }

        }

    }
}
