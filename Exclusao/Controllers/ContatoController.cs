using FiapTechChallenge.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Exclusao.Controllers
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
        /// Deleta um contato por Id
        /// </summary>
        /// <param name="id">Id do Contato</param>
        /// <response code="200">Contato deletado com sucesso</response>
        /// <response code="400">Erro ao deletar o contato</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _contatoService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

    }
}
