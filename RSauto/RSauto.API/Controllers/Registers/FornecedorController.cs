using Microsoft.AspNetCore.Mvc;
using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Contracts.Services.Registers;
using RSauto.Domain.Entities.Cadastro.Fornecedor.Input;
using System.Threading.Tasks;

namespace RSauto.API.Controllers.Registers
{
    public class FornecedorController : BaseApiController
    {
        private readonly IFornecedorService _service;

        public FornecedorController(IFornecedorService service)
        {
            _service = service;
        }

        [HttpPost("Create")]        
        public async Task<IActionResult> Create([FromBody] FornecedorInput input)
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
        public async Task<IActionResult> Update(int id, [FromBody] FornecedorInput input)
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
        public async Task<IActionResult> Listar([FromQuery] FornecedorListarInput input)
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
