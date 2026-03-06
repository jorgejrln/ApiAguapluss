using ApiAguapluss.DTO_s;
using ApiAguapluss.Modelos;
using ApiAguapluss.repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ApiAguapluss.Controllers
{
    [Route("Usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuariosSQL repositorio;

        public UsuariosController(UsuariosSQL r)
        {
            this.repositorio = r;
        }


        [HttpPost]
        public async Task<ActionResult> InsertarUsuario(UsuarioDTO r)
        {
           var usuario = await repositorio.insertaUsuario(r);
            return Ok(new { idUsuario = usuario });


        }

        [HttpGet]
        public async Task<IEnumerable<Usuario>> DameUsuarios()
        { 
            var usuario = await repositorio.dameUsuarios();
            return usuario;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string usuario, string password)
        {
            var user = await repositorio.validarLogin(usuario, password);

            if (user == null)
            {
                return Unauthorized("Usuario o contraseña incorrectos");
            }

            return Ok(user);
        }

    }
}
