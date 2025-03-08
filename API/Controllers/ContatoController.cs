using Core.Interfaces.Services;
using Core.Requests.Create;
using Core.Requests.Update;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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
