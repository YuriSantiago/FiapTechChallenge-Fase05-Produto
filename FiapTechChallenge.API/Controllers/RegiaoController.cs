using FiapTechChallenge.Core.Interfaces.Services;
using FiapTechChallenge.Core.Requests.Create;
using FiapTechChallenge.Core.Requests.Update;
using Microsoft.AspNetCore.Mvc;

namespace FiapTechChallenge.API.Controllers
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
                return BadRequest(ex);
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
                return BadRequest(ex);
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
                return BadRequest(ex);
            }
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
                return BadRequest(ex);
            }

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
                return BadRequest(ex);
            }
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
                return BadRequest(ex);
            }
        }

    }
}
