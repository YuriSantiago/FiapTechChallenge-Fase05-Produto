using Core.Requests.Update;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace CadastroProdutor.Controllers
{

    [ApiController]
    [Route("/[controller]")]
    public class RegiaoController : ControllerBase
    {

        private readonly IBus _bus;
        private readonly IConfiguration _configuration;

        public RegiaoController(IBus bus, IConfiguration configuration)
        {
            _bus = bus;
            _configuration = configuration;
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
        public async Task<IActionResult> Put([FromBody] RegiaoUpdateRequest regiaoUpdateRequest)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var nomeFila = _configuration.GetSection("MassTransit:Queues")["RegiaoQueue"] ?? string.Empty;
                var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{nomeFila}"));
                await endpoint.Send(regiaoUpdateRequest);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

    }
}
