using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Contracts.Services.Registers;
using RSauto.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSauto.API.Controllers.Registers
{    
    public class MarcasPecasController : BaseApiController
    {
        private readonly IMarcasPecasService _service;

        public MarcasPecasController(IMarcasPecasService service)
        {
            _service = service;
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Insert([FromBody] string nome)
        {
            ICommandResult retorno = await _service.Create(nome);

            if (retorno.Sucesso)
                return Ok(retorno);
            else if (!retorno.Sucesso && retorno.Dados != null)
                return UnprocessableEntity(retorno);
            else
                return BadRequest(retorno);
        }

        [HttpPut("Update/{id:int}")]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] string nome)
        {
            ICommandResult retorno = await _service.Update(new MarcasPecasEntity { ID_MARCA_PECAS = id, DESCRICAO = nome });

            if (retorno.Sucesso)
                return Ok(retorno);
            else if (!retorno.Sucesso && retorno.Dados != null)
                return UnprocessableEntity(retorno);
            else
                return BadRequest(retorno);
        }

        //[HttpDelete("Delete/{id:int}")]
        //[ProducesResponseType(typeof(ICommandResult), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ICommandResult), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(ICommandResult), StatusCodes.Status422UnprocessableEntity)]
        //[ProducesResponseType(typeof(ICommandResult), StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Remove(int id)
        //{
        //    ICommandResult retorno = await _service.Remove(id);

        //    if (retorno.Sucesso)
        //        return Ok(retorno);
        //    else if (!retorno.Sucesso && retorno.Dados != null)
        //        return UnprocessableEntity(retorno);
        //    else
        //        return BadRequest(retorno);
        //}

        [HttpGet("Listar")]
        [ProducesResponseType(typeof(IEnumerable<ModelosVeiculosEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Listar()
        {
            ICommandResult retorno = await _service.Listar();

            if (retorno.Sucesso)
                return Ok(retorno);
            else if (!retorno.Sucesso && retorno.Dados != null)
                return UnprocessableEntity(retorno);
            else
                return BadRequest(retorno);
        }
    }
}
