using FiapTechChallenge.Core.Requests.Create;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace CadastroProdutor.Controllers
{

    [ApiController]
    [Route("/Regiao")]
    public class RegiaoController : ControllerBase
    {

        [HttpPost]
        public IActionResult Post([FromBody] RegiaoRequest regiaoRequest)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
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
                        queue: "filaCadastroRegiao",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var message = JsonSerializer.Serialize(regiaoRequest);

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish
                        (
                         exchange: "",
                         routingKey: "filaCadastroRegiao",
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
