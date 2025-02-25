using FiapTechChallenge.Core.Interfaces.Services;
using FiapTechChallenge.Core.Requests.Create;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace CadastroProdutor.Controllers
{

    [ApiController]
    [Route("/Contato")]
    public class ContatoController : ControllerBase
    {
        private readonly IRegiaoService _regiaoService;

        public ContatoController(IRegiaoService regiaoService)
        {
            _regiaoService = regiaoService;
        }

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

                var factory = new ConnectionFactory()
                {
                    HostName = "localhost",
                    UserName = "guest",
                    Password = "guest"
                };

                using var connection = factory.CreateConnection();

                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: "filaCadastroContato",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var message = JsonSerializer.Serialize(contatoRequest);

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish
                        (
                         exchange: "",
                         routingKey: "filaCadastroContato",
                         basicProperties: null,
                         body: body
                        );
                };

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

    }
}
