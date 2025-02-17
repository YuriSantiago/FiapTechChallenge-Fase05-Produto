using FiapTechChallenge.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Consulta.Controllers
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
        /// Busca todas as regiões cadastradas
        /// </summary>
        /// <returns>Retorna todas as regiões cadastradas</returns>
        /// <response code="200">Listagem retornada com sucesso</response>
        /// <response code="400">Erro ao listar as regiões</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_regiaoService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Busca uma região por Id
        /// </summary>
        /// <param name="id">Id da região</param>
        /// <returns>Retorna uma região filtrada por um Id</returns>
        /// <response code="200">Região retornada com sucesso</response>
        /// <response code="400">Erro ao buscar a região</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {
            try
            {
                return Ok(_regiaoService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Busca uma região por DDD
        /// </summary>
        /// <param name="ddd">DDD da região</param>
        /// <returns>Retorna uma região filtrada por um DDD</returns>
        /// <response code="200">Região retornada com sucesso</response>
        /// <response code="400">Erro ao buscar a região</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet("getbyDDD/{ddd}")]
        public IActionResult Get([FromRoute] short ddd)
        {
            try
            {
                var regiaoDTO = _regiaoService.GetByDDD(ddd);

                if (regiaoDTO is not null)
                    return Ok(regiaoDTO);

                return NotFound("Região não encontrada");
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

    }
}
