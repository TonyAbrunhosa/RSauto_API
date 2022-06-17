using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Contracts.Services.Cadastros;
using RSauto.Domain.Entities;
using RSauto.Domain.Entities.Command;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSauto.API.Controllers.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AnoModeloVeiculoController : Controller
    {
        private readonly IAnoModeloVeiculoService _anoModeloVeiculoService;
        private readonly IAnoModeloVeiculoQueryService _anoModeloVeiculoQueryService;

        public AnoModeloVeiculoController(IAnoModeloVeiculoService anoModeloVeiculoService, IAnoModeloVeiculoQueryService anoModeloVeiculoQueryService)
        {
            _anoModeloVeiculoService = anoModeloVeiculoService;
            _anoModeloVeiculoQueryService = anoModeloVeiculoQueryService;
        }

        [HttpPost("Novo")]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Novo([FromBody] string nome)
        {
            try
            {
                ICommandResult retorno = await _anoModeloVeiculoService.Novo(nome);

                if (retorno.Sucesso)
                    return Ok(retorno);
                else if (!retorno.Sucesso && retorno.Dados != null)
                    return UnprocessableEntity(retorno);
                else
                    return BadRequest(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new CommandResult(false, ex.Message + ex.StackTrace));
            }
        }

        [HttpPut("Atualizar/{id:int}")]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Atualizar([FromBody] string nome, int id)
        {
            try
            {
                ICommandResult retorno = await _anoModeloVeiculoService.Atualizar(new AnoModeloVeiculoEntity { ID_ANO_MOD_VEIC = id, DESCRICAO = nome });

                if (retorno.Sucesso)
                    return Ok(retorno);
                else if (!retorno.Sucesso && retorno.Dados != null)
                    return UnprocessableEntity(retorno);
                else
                    return BadRequest(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new CommandResult(false, ex.Message + ex.StackTrace));
            }
        }

        [HttpDelete("Remover/{id:int}")]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                ICommandResult retorno = await _anoModeloVeiculoService.Remover(id);

                if (retorno.Sucesso)
                    return Ok(retorno);
                else if (!retorno.Sucesso && retorno.Dados != null)
                    return UnprocessableEntity(retorno);
                else
                    return BadRequest(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new CommandResult(false, ex.Message + ex.StackTrace));
            }
        }

        [HttpGet("Listar")]
        [ProducesResponseType(typeof(IEnumerable<ModelosVeiculosEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Listar()
        {
            try
            {
                ICommandResult retorno = await _anoModeloVeiculoQueryService.Listar();

                if (retorno.Sucesso)
                    return Ok(retorno);
                else if (!retorno.Sucesso && retorno.Dados != null)
                    return UnprocessableEntity(retorno);
                else
                    return BadRequest(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new CommandResult(false, ex.Message + ex.StackTrace));
            }
        }
    }
}
