using System.Xml.Schema;
using ApiAguapluss.DTO_s;
using ApiAguapluss.Modelos;
using ApiAguapluss.repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAguapluss.Controllers
{
    [Route("Cargas")]
    [ApiController]
    public class CargasController : ControllerBase
    {
        private readonly CargasSQL repositorio;

        public CargasController(CargasSQL r) {
            this.repositorio = r;
        }


        [HttpGet]
        public async Task<IEnumerable<Carga>> DameCargas() { 
        
        var cargas = repositorio.DameCargas();
            return await repositorio.DameCargas();
        
        }
     
        [HttpPost]
        public async Task<ActionResult> InsertaCarga(CargasDTO c)
        {
            var id = await repositorio.CrearCarga(c);
            return Ok(new { idCarga = id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CargasDTO>>ModificarCarga(int id, CargasDTO c) {
        var carga = await repositorio.ModificarCarga(id, c);
            return c;
        
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCarga(int id)
        {
            var carga = await repositorio.EliminarCarga(id);
            if (!carga) 
            {
                return NotFound("No se encontró la carga.");
            }


            return NoContent();
        }

        [HttpGet("{idAguador}")]
        public async Task<ActionResult<IEnumerable<Carga>>> DameCargasPorAguador(int idAguador)
        {
            var cargas = await repositorio.DameCargaIdAguador(idAguador);

            if (cargas == null || !cargas.Any())
            {
                return NotFound("No se encontraron cargas para este aguador.");
            }

            return Ok(cargas);
        }



    }
}
