using Microsoft.AspNetCore.Mvc;
using RSauto.Domain.Contracts.Command;
using System.Threading.Tasks;
using RSauto.Domain.Contracts.Services.Registers;
using RSauto.Domain.Entities.Cadastro.Cliente.Input;

namespace RSauto.API.Controllers.Registers
{
    public class ClienteController : BaseApiController
    {
        private readonly IClienteService _service;

        public ClienteController(IClienteService service)
        {
            _service = service;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ClienteInput input)
        {
            ICommandResult retorno = await _service.Create(input);

            if (retorno.Sucesso)
                return Ok(retorno);
            else if (!retorno.Sucesso && retorno.Dados != null)
                return UnprocessableEntity(retorno);
            else
                return BadRequest(retorno);
        }

        [HttpPut("Update/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClienteInput input)
        {
            ICommandResult retorno = await _service.Update(id, input);

            if (retorno.Sucesso)
                return Ok(retorno);
            else if (!retorno.Sucesso && retorno.Dados != null)
                return UnprocessableEntity(retorno);
            else
                return BadRequest(retorno);
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> Listar([FromQuery] ClienteListarInput input)
        {
            ICommandResult retorno = await _service.Listar(input);

            if (retorno.Sucesso)
                return Ok(retorno);
            else if (!retorno.Sucesso && retorno.Dados != null)
                return UnprocessableEntity(retorno);
            else
                return BadRequest(retorno);
        }
    }
}
