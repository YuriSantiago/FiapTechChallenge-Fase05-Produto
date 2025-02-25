using FiapTechChallenge.Core.Requests.Update;
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

        [HttpPut]
        public IActionResult Put([FromBody] RegiaoUpdateRequest regiaoUpdateRequest)
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
                        queue: "filaAtualizacaoRegiao",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var message = JsonSerializer.Serialize(regiaoUpdateRequest);

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish
                        (
                         exchange: "",
                         routingKey: "filaAtualizacaoRegiao",
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
