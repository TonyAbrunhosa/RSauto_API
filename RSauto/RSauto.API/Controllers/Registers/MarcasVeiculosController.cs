﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSauto.Domain.Contracts.Command;
using RSauto.Domain.Contracts.Services.Registers;
using RSauto.Domain.Entities;
using RSauto.Domain.Entities.Command;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSauto.API.Controllers.Registers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class MarcasVeiculosController : Controller
    {
        private readonly IMarcasVeiculosService _marcasVeiculosService;
        private readonly IMarcasVeiculosQueryService _marcasVeiculosQueryService;

        public MarcasVeiculosController(IMarcasVeiculosService marcasVeiculosService, IMarcasVeiculosQueryService marcasVeiculosQueryService)
        {
            _marcasVeiculosService = marcasVeiculosService;
            _marcasVeiculosQueryService = marcasVeiculosQueryService;
        }

        [HttpPost("Insert")]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Insert([FromBody] string nome)
        {
            try
            {
                ICommandResult retorno = await _marcasVeiculosService.Insert(nome);

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

        [HttpPut("Update/{id:int}")]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] string nome, int id)
        {
            try
            {
                ICommandResult retorno = await _marcasVeiculosService.Update(new MarcasVeiculosEntity { ID_MARCA = id, NOME = nome });

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

        [HttpDelete("Remove/{id:int}")]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                ICommandResult retorno = await _marcasVeiculosService.Remove(id);

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
        [ProducesResponseType(typeof(IEnumerable<MarcasVeiculosEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ICommandResult), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Listar()
        {
            try
            {
                ICommandResult retorno = await _marcasVeiculosQueryService.Listar();

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
