using System.Data.SqlClient;
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
        public async Task<ActionResult> CrearTurno([FromBody] TurnoModeloEmpezar t)
        {
            try
            {
                TurnoModeloEmpezar turno = new TurnoModeloEmpezar
                {
                    fecha = DateTime.Now,
                    idTrabajador = t.idTrabajador,
                    lecIn = t.lecIn,
                    fondo = t.fondo,

                };

                await this.repositorio.CrearTurno(turno);

                return Ok(turno);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error al crear el turno");
            }
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
        public async Task<Turno> UltimoTurno()
        {
            var turno = await repositorio.DameUltimoTurno();
            return turno;

        }


        [HttpPut("CerrarTurno")]
        public async Task<ActionResult> CerrarTurno(TurnoTerminar t)
        {
            await this.repositorio.CerrarTurno(t);
            return Ok();
        }


        [HttpGet("Ultimos3")]
        public async Task<IEnumerable<Turno>> Ultimos3Turnos() 
        {
            var turnos = await repositorio.Dame3UltimosTurnos();
            return turnos;
        }
    }
}
