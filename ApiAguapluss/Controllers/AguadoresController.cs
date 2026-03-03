using ApiAguapluss.DTO_s;
using ApiAguapluss.Modelos;
using ApiAguapluss.repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAguapluss.Controllers
{
    [Route("Aguadores")]
    [ApiController]
    public class AguadoresController : ControllerBase
    {
        private readonly AguadoresSQL repositorio;

        public AguadoresController(AguadoresSQL r)
        {
            this.repositorio = r;
        }

        [HttpGet]
        public async Task<IEnumerable<Aguadores>> DameAguadores()
        {
            var aguadores = await repositorio.DameTrabajadores();
            return aguadores;
        }


        [HttpPost]
        public async Task<int> crearAguador(AguadorDTO a)
        {
            var aguador = await repositorio.CrearTrabajador(a);
            return aguador;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AguadorDTO>> modificarAguador(int id, AguadorDTO a)
        {
            var aguador = await repositorio.ModificarAguador(id, a);
            return a;
        }

    }
}
