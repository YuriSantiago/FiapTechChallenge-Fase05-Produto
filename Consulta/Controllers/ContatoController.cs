using FiapTechChallenge.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Consulta.Controllers
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
        /// Busca todos os contatos cadastrados
        /// </summary>
        /// <returns>Retorna todos os contatos cadastrados</returns>
        /// <response code="200">Listagem retornada com sucesso</response>
        /// <response code="400">Erro ao listar os contatos</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_contatoService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Busca um contato por Id
        /// </summary>
        /// <param name="id">Id do Contato</param>
        /// <returns>Retorna um contato filtrado por um Id</returns>
        /// <response code="200">Contato retornado com sucesso</response>
        /// <response code="400">Erro ao buscar o contato</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {
            try
            {
                return Ok(_contatoService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Busca constatos por DDD
        /// </summary>
        /// <param name="ddd">DDD dos Contatos</param>
        /// <returns>Retorna uma lista de contatos filtrada por um DDD</returns>
        /// <response code="200">Contatos retornados com sucesso</response>
        /// <response code="400">Erro ao buscar a lista de contatos</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet("getbyDDD/{ddd}")]
        public IActionResult Get([FromRoute] short ddd)
        {
            try
            {
                return Ok(_contatoService.GetAllByDDD(ddd));
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

    }
}
