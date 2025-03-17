using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Prometheus;

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

        [Function("BuscarTodosContatos")]
        public IActionResult GetAll([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "contatos")] HttpRequest req)
        {
            _logger.LogInformation("Função para buscar todos os contatos");
            return new OkObjectResult(_contatoService.GetAll());
        }

        [Function("BuscarContatoPorId")]
        public IActionResult GetById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "contatos/id/{id}")] HttpRequest req, int id)
        {
            _logger.LogInformation("Função para buscar um contato por ID");

            try
            {
                return new OkObjectResult(_contatoService.GetById(id));
            }
            catch
            {
                return new NotFoundObjectResult($"Nenhum contato encontrado com o ID: '{id}'.");
            }

        }

        [Function("BuscarContatosPorDDD")]
        public IActionResult GetByDDD([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "contatos/ddd/{ddd}")] HttpRequest req, short ddd)
        {
            _logger.LogInformation("Função para buscar um contato por DDD");
            return new OkObjectResult(_contatoService.GetAllByDDD(ddd));
        }

    }
}


