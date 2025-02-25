using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace CadastroProdutor.Controllers
{

    [ApiController]
    [Route("/Regiao")]
    public class RegiaoController : ControllerBase
    {

        [HttpDelete]
        public IActionResult Put([FromRoute] int id)
        {

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
                        queue: "filaExclusaoRegiao",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var message = JsonSerializer.Serialize(id);

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish
                        (
                         exchange: "",
                         routingKey: "filaExclusaoRegiao",
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
