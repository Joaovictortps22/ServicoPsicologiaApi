using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using servico_atendimento_psicologia.Manager;
using servico_atendimento_psicologia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
                return StatusCode(200, "Usuario cadastrado com sucesso");
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
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                List<Claim> claim = identity.Claims.ToList();

                var papelUsuario = claim[0].Value;

                _manager.Deletar(email);
                return StatusCode(200, "Usuário Deletado com sucesso");
            }
            catch(Exception ex)
            {
                return StatusCode(401, new { message = ex.Message });
            }
        }
        [HttpPost("Login")]
        public ActionResult Login([FromBody] LoginDto loginUsuario)
        {
            try
            {
                return StatusCode(200, _manager.LoginUsuario(loginUsuario));
            }
            catch(Exception ex)
            {
                return StatusCode(401, new { message = ex.Message });
            }
        }
        [HttpGet("ListarPapeis/{IdUsuario}")]
        public ActionResult ListarPapeis(int IdUsuario)
        {
            try
            {
                return StatusCode(200, _manager.ListarPapeis(IdUsuario));
            }
            catch (Exception ex)
            {
                return StatusCode(401, new { message = ex.Message });
            }
        }
        [HttpPut("Atualizar/{IdUsuario}")]
        public ActionResult Atualizar([FromBody] UsuarioDto usuario)
        {
            try
            {
                _manager.Atualizar(usuario);
                return StatusCode(200, "Usuario atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(401, new { message = ex.Message });
            }
        }
    }
}
