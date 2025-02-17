using FiapTechChallenge.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Exclusao.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ContatoController : ControllerBase
    {

        private readonly IContatoService _contatoService;

        public ContatoController(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }

        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _contatoService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

    }
}
