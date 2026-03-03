using ApiAguapluss.DTO_s;
using ApiAguapluss.Modelos;
using ApiAguapluss.repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAguapluss.Controllers
{
    [Route("Turnos")]
    [ApiController]
    public class TurnosController : ControllerBase
    {
        private readonly TurnoSQL repositorio;


        public TurnosController(TurnoSQL r) {
            this.repositorio = r;
        }

        [HttpPost]
        public async Task<ActionResult> CrearTurno(TurnoDTO t) {

            TurnoDTO turno = new TurnoDTO
            {
                fecha = DateTime.Now,
                idTrabajador = t.idTrabajador,
                lecIn = t.lecIn,
                lecFin = t.lecFin,
                total = t.total,
                fondo = t.fondo,
                corte = t.corte,
            };


            await this.repositorio.CrearTurno(turno);
            return Ok(turno);
        }

        [HttpGet]
        public async Task<IEnumerable<Turno>> DameTurnos() {

            var listaTurnos = (await repositorio.DameTurnos());
            return listaTurnos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Turno>> DameTurnoID(int id) {
            var turno = await repositorio.TurnoId(id);

            if (turno == null)
                return NotFound();

            return Ok(turno);
        }

        [HttpPut]
        public async Task<ActionResult<Turno>> ModificarTurno(int id, TurnoDTO t) {

            await repositorio.ModificarTurno(id, t);
            return NoContent();

        }

        [HttpDelete]
        public async Task<ActionResult<Turno>> EliminarTurno(int id)
        {
            await repositorio.EliminarTurno(id);
            return NoContent();
        }

        [HttpGet("trabajador/{id:int}")]
        public async Task<IActionResult> TurnosTrabajador(int id)
        {
            var turnos = await repositorio.DameTurnosTrabajador(id);

            return Ok(new
            {
                idRecibido = id,
                cantidad = turnos.Count(),
                datos = turnos
            });
        }

        [HttpGet("Ultimo")]
        public async Task<Turno>UltimoTurno()
        {
            var turno = await repositorio.DameUltimoTurno();
            return turno;

        }

    }
}
