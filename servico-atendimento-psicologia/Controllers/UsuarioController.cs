using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using servico_atendimento_psicologia.Manager;
using servico_atendimento_psicologia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace servico_atendimento_psicologia.Controllers
{
    [ApiController]
    [Route("Usuario")]
    public class UsuarioController : Controller
    {
        private readonly UsuarioManager _manager = new UsuarioManager();
        //[ProducesResponseType(typeof(ResponseJWTDTO), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(BadRequestResponseErrors), 400)]
        //[ProducesResponseType(typeof(RequestResponseErrors), 401)]
        //[ProducesResponseType(typeof(RequestResponseErrors), 500)]
        //[SwaggerOperation(Summary = "Método para fazer Login",
        //    Description = "Método ultiliza login e senha para verificar se existe o mesmo na base.")]
        [HttpGet("GetUsuarios")]
        public ActionResult GetUsuarios()
        {
            try
            {
                return StatusCode(200, _manager.GetUsuarios());
            }
            catch(Exception ex)
            {
                return StatusCode(401, new { message = ex.Message });
            }
        }
        [HttpGet("GetUsuario/{IdUsuario}")]
        public ActionResult GetUsuario(int IdUsuario)
        {
            try
            {
                return StatusCode(200, _manager.GetUsuario(IdUsuario));
            }
            catch(Exception ex)
            {
                return StatusCode(401, new { message = ex.Message });
            }
        }
        [HttpPost("Cadastrar")]
        public ActionResult Cadastrar([FromBody] UsuarioDto usuario)
        {
            try
            {
                _manager.Cadastrar(usuario);
                return StatusCode(200, "Usuario Cadastrado com sucesso");
            }
            catch(Exception ex)
            {
                return StatusCode(401, new { message = ex.Message });
            }
        }
        [Authorize]
        [HttpDelete("Deletar/{email}")]
        public ActionResult Deletar(string email)
        {
            try
            {
                _manager.Deletar(email);
                return StatusCode(200, "Usuário Deletado com sucesso");
            }
            catch(Exception ex)
            {
                return StatusCode(401, new { message = ex.Message });
            }
        }
        [HttpPost("Login")]
        public ActionResult Login([FromBody] UsuarioDto usuario)
        {
            try
            {
                return StatusCode(200, _manager.LoginUsuario(usuario));
            }
            catch(Exception ex)
            {
                return StatusCode(401, new { message = ex.Message });
            }
        }
    }
}
