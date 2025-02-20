using FiapTechChallenge.Core.Interfaces.Services;
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

        public ConsultaRegioes(IRegiaoService regiaoService,ILogger<ConsultaRegioes> logger)
        {
            _regiaoService = regiaoService;
            _logger = logger;
        }

        [Function("ConsultaRegioes")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("Função para processamento de listagem das regiões");
            return new OkObjectResult(_regiaoService.GetAll());
        }
    }
}
