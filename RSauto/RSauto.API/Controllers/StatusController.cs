using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace RSauto.API.Controllers
{
    public class StatusController : BaseApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Content($"<div style=\"border-width: 2px; border-style: dashed; padding: 10px\">" +
                   "API Master Retail Autorizacao v.: 1.0.0 20-03-2020 <br/>" +
                   $"Data e hora servidor: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}<br/>" +
                   $"{Environment.MachineName}<br/>" +
                   $"Porta: {this.HttpContext.Connection.LocalPort.ToString()}<br/>" +
                   $"</div>", "text/html");
        }
    }
}
