using Core.Interfaces.Services;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ConsultaFunction
{
    public class ConsultaRegioes
    {

        private readonly IRegiaoService _regiaoService;
        private readonly ILogger<ConsultaRegioes> _logger;

        public ConsultaRegioes(IRegiaoService regiaoService, ILogger<ConsultaRegioes> logger)
        {
            _regiaoService = regiaoService;
            _logger = logger;
        }

        [Function("BuscarTodasRegioes")]
        public IActionResult GetAll([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "regioes")] HttpRequest req)
        {
            _logger.LogInformation("Função para buscar todas as regiões");
            return new OkObjectResult(_regiaoService.GetAll());
        }

        [Function("BuscarRegiaoPorId")]
        public IActionResult GetById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "regioes/id/{id}")] HttpRequest req, int id)
        {
            _logger.LogInformation("Função para buscar uma região por ID");

            try
            {
                return new OkObjectResult(_regiaoService.GetById(id));
            }
            catch
            {
                return new NotFoundObjectResult($"Nenhunha região encontrada com o ID: '{id}'.");
            }

        }

        [Function("BuscarRegiaoPorDDD")]
        public IActionResult GetByDDD([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "regioes/ddd/{ddd}")] HttpRequest req, short ddd)
        {
            _logger.LogInformation("Função para buscar uma região por DDD");
            var regiao = _regiaoService.GetByDDD(ddd);

            if (regiao is null)
                return new NotFoundObjectResult($"Nenhunha região encontrada com o DDD: '{ddd}'.");

            return new OkObjectResult(regiao);
        }

    }
}
