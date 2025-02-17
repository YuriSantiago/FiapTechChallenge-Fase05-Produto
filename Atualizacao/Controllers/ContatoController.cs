using FiapTechChallenge.Core.Interfaces.Services;
using FiapTechChallenge.Core.Requests.Update;
using Microsoft.AspNetCore.Mvc;

namespace Atualizacao.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ContatoController : ControllerBase
    {

        private readonly IContatoService _contatoService;

        public ContatoController(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }

        /// <summary>
        /// Atualiza as informações de um contato
        /// </summary>
        /// <param name="contatoUpdateRequest">Objeto do tipo "ContatoUpdateRequest"</param>
        /// <response code="200">Contato atualizado com sucesso</response>
        /// <response code="400">Erro ao atualizar o contato</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPut]
        public IActionResult Put([FromBody] ContatoUpdateRequest contatoUpdateRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _contatoService.Put(contatoUpdateRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

    }
}
