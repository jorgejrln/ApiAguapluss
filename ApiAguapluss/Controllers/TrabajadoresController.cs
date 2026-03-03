using ApiAguapluss.DTO_s;
using ApiAguapluss.Modelos;
using ApiAguapluss.repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAguapluss.Controllers
{
    [Route("Trabajadores")]
    [ApiController]
    public class TrabajadoresController : ControllerBase
    {
        private readonly TrabajadoresSQL repositorio;
        public TrabajadoresController(TrabajadoresSQL r)
        {
            this.repositorio = r;
        }

        [HttpGet]
        public async Task<IEnumerable<Trabajador>> DameTrabajadores()
        {
            var trabajadores = await repositorio.DameTrabajadores();
            return trabajadores;
        }

        [HttpPost]
        public async Task<int> CrearAguador(TrabajadorDTO t)
        {
            var trabajador = await repositorio.CrearTrabajador(t);
            return trabajador;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TrabajadorDTO>> ModificarCarga(int id, TrabajadorDTO t)
        {
            var trabajador = await repositorio.ModificiarTrabajador(id, t);
            return t;

        }

    }
}
