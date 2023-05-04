using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Contracts.Services;
using RSauto.Domain.Entities.Token.Input;
using System.Threading.Tasks;

namespace RSauto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _service;

        public TokenController(ITokenService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost("Web")]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterToken([FromBody] DadosTokenInput input)
        {
            ICommandResult retorno = await _service.ObterToken(input);

            if (retorno.Sucesso)
                return Ok(retorno);
            else if (!retorno.Sucesso && retorno.Dados != null)
                return UnprocessableEntity(retorno);
            else
                return BadRequest(retorno);
        }
    }
}
