using ApiAguapluss.DTO_s;
using ApiAguapluss.Modelos;
using ApiAguapluss.repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAguapluss.Controllers
{
    [Route("roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RolesSQL repositorio;

        public RolesController(RolesSQL r)
        {
            this.repositorio = r;
        }



        [HttpGet]
        public async Task<IEnumerable<Rol>> GetRoles()
        {
            var rol = await repositorio.DameRoles();
            return rol;

        }



        [HttpPost]
        public async Task<ActionResult> InsertarRol(RolDTO r)
        {
            var id = await repositorio.insertaRol(r);
            return Ok(new { idRol = id });

        }

        }
    }
