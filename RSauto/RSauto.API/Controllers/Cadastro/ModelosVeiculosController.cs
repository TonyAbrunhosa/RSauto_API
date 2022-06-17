using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Contracts.Services.Cadastros;
using RSauto.Domain.Entities;
using RSauto.Domain.Entities.Cadastro.ModelosVeiculos.input;
using RSauto.Domain.Entities.Command;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSauto.API.Controllers.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ModelosVeiculosController : Controller
    {
        private readonly IModelosVeiculosService _modelosVeiculosService;
        private readonly IModelosVeiculosQueryService _modelosVeiculosQueryService;

        public ModelosVeiculosController(IModelosVeiculosService modelosVeiculosService, IModelosVeiculosQueryService modelosVeiculosQueryService)
        {
            _modelosVeiculosService = modelosVeiculosService;
            _modelosVeiculosQueryService = modelosVeiculosQueryService;
        }

        [HttpPost("Novo")]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Novo([FromBody] ModelosVeiculosInput input)
        {
            try
            {
                ICommandResult retorno = await _modelosVeiculosService.Novo(input);

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
        public async Task<IActionResult> Atualizar([FromBody] ModelosVeiculosInput input, int id)
        {
            try
            {
                ICommandResult retorno = await _modelosVeiculosService.Atualizar(id, input);

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
                ICommandResult retorno = await _modelosVeiculosService.Remover(id);

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
                ICommandResult retorno = await _modelosVeiculosQueryService.Listar();

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
