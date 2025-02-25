using FiapTechChallenge.Core.Requests.Update;
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
       
        [HttpPut]
        public IActionResult Put([FromBody] ContatoUpdateRequest contatoUpdateRequest)
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
                        queue: "filaAtualizacaoContato",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var message = JsonSerializer.Serialize(contatoUpdateRequest);

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish
                        (
                         exchange: "",
                         routingKey: "filaAtualizacaoContato",
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
