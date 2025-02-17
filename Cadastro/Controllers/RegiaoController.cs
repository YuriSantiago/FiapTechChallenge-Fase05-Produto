using FiapTechChallenge.Core.Interfaces.Services;
using FiapTechChallenge.Core.Requests.Create;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.Controllers
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
        /// Cadastra uma nova região 
        /// </summary>
        /// <param name="regiaoRequest">Objeto do tipo "RegiaoRequest"</param>
        /// <response code="200">Região cadastrada com sucesso</response>
        /// <response code="400">Erro ao cadastrar a região</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost]
        public IActionResult Post([FromBody] RegiaoRequest regiaoRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _regiaoService.Create(regiaoRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }

        }

    }
}
