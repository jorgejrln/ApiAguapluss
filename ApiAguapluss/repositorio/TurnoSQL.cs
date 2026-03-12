using System.Data;
using System.Data.SqlClient;
using ApiAguapluss.DTO_s;
using ApiAguapluss.Modelos;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ApiAguapluss.repositorio

{
    public class TurnoSQL
    {
        private String CadenaConexion;

        public TurnoSQL(AcessoSQL cadenaConexion) {

            CadenaConexion = cadenaConexion.CadenaConexionSQL;
        }
        private SqlConnection conexion() {

            return new SqlConnection(CadenaConexion);
        }
        public async Task CrearTurno(TurnoModeloEmpezar t) {

            SqlConnection sqlConnection = conexion();
            SqlCommand Comm = null;
            try
            {
                await sqlConnection.OpenAsync();
                Comm = sqlConnection.CreateCommand();
                Comm.CommandText = "dbo.CrearTurno";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@idTrabajador", SqlDbType.Int).Value = t.idTrabajador;
                Comm.Parameters.Add("@lecIn", SqlDbType.Int).Value = t.lecIn;
                Comm.Parameters.Add("@fondo", SqlDbType.Int).Value = t.fondo;
             

                await Comm.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("se produjo un error al insertar el turno" + ex.ToString());
            }
            finally
            {
                Comm.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            await Task.CompletedTask;
        }
        public async Task<IEnumerable<Turno>> DameTurnos() {
            SqlConnection sqlConnection = conexion();
            SqlCommand Comm = null;
            Turno t = null;
            List<Turno> turnos = new List<Turno>();
            try
            {
                await sqlConnection.OpenAsync();
                Comm = sqlConnection.CreateCommand();
                Comm.CommandText = "ObtenerTurnosConTrabajador";
                Comm.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    t = new Turno
                    {
                        id = Convert.ToInt32(reader["idTurno"]),
                        idTrabajador = Convert.ToInt32(reader["idTrabajador"]),

                        fecha = reader.IsDBNull(reader.GetOrdinal("fecha"))
        ? null
        : reader.GetDateTime(reader.GetOrdinal("fecha")),

                        lecIn = Convert.ToInt32(reader["lecIn"]),

                        lecFin = reader.IsDBNull(reader.GetOrdinal("lecFin"))
        ? 0
        : reader.GetInt32(reader.GetOrdinal("lecFin")),

                        total = reader.IsDBNull(reader.GetOrdinal("total"))
        ? 0
        : reader.GetInt32(reader.GetOrdinal("total")),

                        fondo = Convert.ToInt32(reader["fondo"]),

                        corte = reader.IsDBNull(reader.GetOrdinal("corte"))
        ? 0
        : reader.GetInt32(reader.GetOrdinal("corte")),

                        nombreTrabajador = reader["NombreTrabajador"].ToString(),

                        activo = Convert.ToBoolean(reader["activo"]), // <-- línea agregada



                        fechaFin = reader.IsDBNull(reader.GetOrdinal("fechaFin"))
        ? null
        : reader.GetDateTime(reader.GetOrdinal("fechaFin")),


                    };
                    turnos.Add(t);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("se produjo un error al obtener los turnos" + ex.ToString());
            }
            finally
            {
                Comm?.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return turnos;
        }
        public async Task<Turno> TurnoId(int id) {
            SqlConnection sqlConnection = conexion();
            SqlCommand Comm = null;
            Turno t = null;

            try
            {
                await sqlConnection.OpenAsync();

                Comm = sqlConnection.CreateCommand();
                Comm.CommandText = "ObtenerTurnoPorId";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@idTurno", SqlDbType.Int).Value = id;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    return new Turno
                    {
                        id = Convert.ToInt32(reader["idTurno"]),
                        idTrabajador = Convert.ToInt32(reader["idTrabajador"]),
                        fecha = reader.IsDBNull(reader.GetOrdinal("fecha"))
                                                                     ? null
         : reader.GetDateTime(reader.GetOrdinal("fecha")),
                        lecIn = Convert.ToInt32(reader["lecIn"]),
                        lecFin = Convert.ToInt32(reader["lecFin"]),
                        total = Convert.ToInt32(reader["total"]),
                        fondo = Convert.ToInt32(reader["fondo"]),
                        corte = Convert.ToInt32(reader["corte"]),
                        nombreTrabajador = reader["NombreTrabajador"].ToString()
                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("se produjo un error al obtener los turnos" + ex.ToString());
            }
            finally
            {
                Comm?.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return null;
        }
        public async Task<Turno> ModificarTurno(int id, TurnoDTO t) {
            SqlConnection sqlConnection = conexion();
            SqlCommand Comm = null;


            try
            {
                await sqlConnection.OpenAsync();

                Comm = sqlConnection.CreateCommand();
                Comm.CommandText = "ModificarTurno";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@idTurno", SqlDbType.Int).Value = id;
                Comm.Parameters.Add("@idTrabajador", SqlDbType.Int).Value = t.idTrabajador;
                Comm.Parameters.Add("@lecIn", SqlDbType.Int).Value = t.lecIn;
                Comm.Parameters.Add("@lecFin", SqlDbType.Int).Value = t.lecFin;
                Comm.Parameters.Add("@total", SqlDbType.Int).Value = t.total;
                Comm.Parameters.Add("@fondo", SqlDbType.Int).Value = t.fondo;
                Comm.Parameters.Add("@corte", SqlDbType.Int).Value = t.corte;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();


            }
            catch (Exception ex)
            {
                throw new Exception("se produjo un error al obtener los turnos" + ex.ToString());
            }
            finally
            {
                Comm?.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return null;
        }
        public async Task<Turno> EliminarTurno(int id) {
            SqlConnection sqlConnection = conexion();
            SqlCommand Comm = null;
            Turno t = null;

            try
            {
                await sqlConnection.OpenAsync();

                Comm = sqlConnection.CreateCommand();
                Comm.CommandText = "EliminarTurno";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@TurnoID", SqlDbType.Int).Value = id;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();

                
              
            }
            catch (Exception ex)
            {
                throw new Exception("se produjo un error al eliminar el turno" + ex.ToString());
            }
            finally
            {
                Comm?.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return null;
        }
        public async Task<IEnumerable<Turno>> DameTurnosTrabajador(int id)
        {
            SqlConnection sqlConnection = conexion();
            SqlCommand Comm = null;
         
            List<Turno> turnos = new List<Turno>();
            try
            {
                await sqlConnection.OpenAsync();

                Comm = sqlConnection.CreateCommand();
                Comm.CommandText = "ObtenerTurnosPorTrabajador";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@idTrabajador", SqlDbType.Int).Value = id;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Turno t = new Turno
                    {
                        id = Convert.ToInt32(reader["idTurno"]),
                        idTrabajador = Convert.ToInt32(reader["idTrabajador"]),
                        fecha = reader.IsDBNull(reader.GetOrdinal("fecha"))
                                                                    ? null
        : reader.GetDateTime(reader.GetOrdinal("fecha")),
                        lecIn = Convert.ToInt32(reader["LecIN"]),
                        lecFin = Convert.ToInt32(reader["LecFIN"]),
                        total = Convert.ToInt32(reader["total"]),
                        fondo = Convert.ToInt32(reader["fondo"]),
                        corte = Convert.ToInt32(reader["corte"])
                    };

                    turnos.Add(t);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("se produjo un error al obtener los turnos" + ex.ToString());
            }
            finally
            {
                Comm?.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return turnos;

        }
        public async Task<Turno> DameUltimoTurno()
        {
            SqlConnection sqlConnection = conexion();
            SqlCommand Comm = null;
            Turno t = null;
            try { 
            await sqlConnection.OpenAsync();
            Comm = sqlConnection.CreateCommand();
            Comm.CommandText = "ObtenerUltimoTurno";
            Comm.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = await Comm.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    t = new Turno
                    {
                        id = Convert.ToInt32(reader["idTurno"]),
                        idTrabajador = Convert.ToInt32(reader["idTrabajador"]),

                        fecha = reader.IsDBNull(reader.GetOrdinal("fecha"))
                            ? null
                            : reader.GetDateTime(reader.GetOrdinal("fecha")),

                        lecIn = Convert.ToInt32(reader["lecIn"]),

                        lecFin = reader.IsDBNull(reader.GetOrdinal("lecFin"))
                            ? 0
                            : reader.GetInt32(reader.GetOrdinal("lecFin")),

                        total = reader.IsDBNull(reader.GetOrdinal("total"))
                            ? 0
                            : reader.GetInt32(reader.GetOrdinal("total")),

                        fondo = Convert.ToInt32(reader["fondo"]),

                        corte = reader.IsDBNull(reader.GetOrdinal("corte"))
                            ? 0
                            : reader.GetInt32(reader.GetOrdinal("corte")),

                        nombreTrabajador = reader["NombreTrabajador"].ToString()
                    };
                }
                reader.Close();
            } 
            catch (Exception ex) 
            {
                throw new Exception("se produjo un error al obtener el turno" + ex.ToString());
            } 
            finally 
            {
                Comm?.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return t;
        }

        public async Task<TurnoTerminar> CerrarTurno(TurnoTerminar t)
        {
            SqlConnection sqlConnection = conexion();

            await sqlConnection.OpenAsync();

            SqlCommand Comm = new SqlCommand("CerrarTurno", sqlConnection);
            Comm.CommandType = CommandType.StoredProcedure;

            Comm.Parameters.AddWithValue("@lecFin", t.lecFin);
            Comm.Parameters.AddWithValue("@total", t.total);
            Comm.Parameters.AddWithValue("@corte", t.corte);

            await Comm.ExecuteNonQueryAsync();

            sqlConnection.Close();

            return t;
        }

        public async Task<IEnumerable<Turno>> Dame3UltimosTurnos()
        {
            SqlConnection sqlConnection = conexion();
            SqlCommand Comm = null;
            Turno t = null;
            List<Turno> turnos = new List<Turno>();
            try
            {
                await sqlConnection.OpenAsync();
                Comm = sqlConnection.CreateCommand();
                Comm.CommandText = "ObtenerUltimos3Turnos";
                Comm.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    t = new Turno
                    {
                        id = Convert.ToInt32(reader["idTurno"]),
                        idTrabajador = Convert.ToInt32(reader["idTrabajador"]),

                        fecha = reader.IsDBNull(reader.GetOrdinal("fecha"))
        ? null
        : reader.GetDateTime(reader.GetOrdinal("fecha")),

                        lecIn = Convert.ToInt32(reader["lecIn"]),

                        lecFin = reader.IsDBNull(reader.GetOrdinal("lecFin"))
        ? 0
        : reader.GetInt32(reader.GetOrdinal("lecFin")),

                        total = reader.IsDBNull(reader.GetOrdinal("total"))
        ? 0
        : reader.GetInt32(reader.GetOrdinal("total")),

                        fondo = Convert.ToInt32(reader["fondo"]),

                        corte = reader.IsDBNull(reader.GetOrdinal("corte"))
        ? 0
        : reader.GetInt32(reader.GetOrdinal("corte")),

                        nombreTrabajador = reader["NombreTrabajador"].ToString(),

                        activo = Convert.ToBoolean(reader["activo"]) // <-- línea agregada
                    };
                    turnos.Add(t);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("se produjo un error al obtener los turnos" + ex.ToString());
            }
            finally
            {
                Comm?.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return turnos;
        }
    }
}
