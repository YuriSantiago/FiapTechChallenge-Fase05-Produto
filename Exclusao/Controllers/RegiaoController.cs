using FiapTechChallenge.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Exclusao.Controllers
{

    [ApiController]
    [Route("/[controller]")]
    public class RegiaoController : ControllerBase
    {
        private readonly IRegiaoService _regiaoService;

        public RegiaoController(IRegiaoService regiaoService)
        {
            _regiaoService = regiaoService;
        }

        /// <summary>
        /// Deleta uma região por Id
        /// </summary>
        /// <param name="id">Id da região</param>
        /// <response code="200">Região deletada com sucesso</response>
        /// <response code="400">Erro ao deletar a região</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _regiaoService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

    }
}
