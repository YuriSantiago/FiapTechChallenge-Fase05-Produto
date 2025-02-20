using FiapTechChallenge.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ConsultaFunction
{
    public class ConsultaContatos
    {
        private readonly IContatoService _contatoService;
        private readonly ILogger<ConsultaContatos> _logger;

        public ConsultaContatos(IContatoService contatoService, ILogger<ConsultaContatos> logger)
        {
            _contatoService = contatoService;
            _logger = logger;
        }

        [Function("ConsultaContatos")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("Função para processamento de listagem dos contatos");
            return new OkObjectResult(_contatoService.GetAll());
        }
    }
}


