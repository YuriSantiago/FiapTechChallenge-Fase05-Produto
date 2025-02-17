using FiapTechChallenge.Core.Interfaces.Services;
using FiapTechChallenge.Core.Requests.Update;
using Microsoft.AspNetCore.Mvc;

namespace Atualizacao.Controllers
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
        /// Atualiza as informações de uma região
        /// </summary>
        /// <param name="regiaoUpdateRequest">Objeto do tipo "RegiaoUpdateRequest"</param>
        /// <response code="200">Região atualizada com sucesso</response>
        /// <response code="400">Erro ao atualizar a região</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPut]
        public IActionResult Put([FromBody] RegiaoUpdateRequest regiaoUpdateRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _regiaoService.Put(regiaoUpdateRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

    }
}
