using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using ApiAguapluss.DTO_s;
using ApiAguapluss.Modelos;

namespace ApiAguapluss.repositorio
{
    public class CargasSQL
    {
        private String CadenaConexion;
        public CargasSQL(AcessoSQL cadenaConexion)
        {
            CadenaConexion = cadenaConexion.CadenaConexionSQL;
        }
        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);

        }



        public async Task<IEnumerable<Carga>> DameCargas()
        {
            SqlConnection sqlConnection = conexion();
            SqlCommand Comm = null;
            Carga c = null;
            List<Carga> cargas = new List<Carga>();

            try
            {
                await sqlConnection.OpenAsync();
                Comm = sqlConnection.CreateCommand();
                Comm.CommandText = "dbo.ObtenerCargas";
                Comm.CommandType = CommandType.Text;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    c = new Carga()
                    {
                        idCarga = Convert.ToInt32(reader["idCarga"]),
                        fecha = reader.IsDBNull(reader.GetOrdinal("fecha")) ? null : reader.GetDateTime(reader.GetOrdinal("fecha")),
                        idAguador = Convert.ToInt32(reader["idAguador"]),
                        garrafones = Convert.ToInt32(reader["garrafones"]),
                        pagado = reader.GetBoolean(reader.GetOrdinal("pagado")),
                        idTurno = Convert.ToInt32(reader["idTurno"])


                    };
                    cargas.Add(c);
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw new Exception("se produjo un error al obtener las Cargas" + ex.ToString());

            }
            finally
            {
                Comm?.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return cargas;
        }
        public async Task<IEnumerable<Carga>> DameCargaIdAguador(int id)
        {
            SqlConnection sqlConnection = conexion();
            SqlCommand Comm = null;
            Carga c = null;
            List<Carga> cargas = new List<Carga>();

            try
            {
                await sqlConnection.OpenAsync();
                Comm = sqlConnection.CreateCommand();
                Comm.CommandText = "dbo.ObtenerCargasPorAguador";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@idAguador", SqlDbType.Int).Value = id;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    c = new Carga()
                    {
                        idCarga = Convert.ToInt32(reader["idCarga"]),
                        fecha = reader.IsDBNull(reader.GetOrdinal("fecha")) ? null : reader.GetDateTime(reader.GetOrdinal("fecha")),
                        idAguador = Convert.ToInt32(reader["idAguador"]),
                        garrafones = Convert.ToInt32(reader["garrafones"]),
                        pagado = reader.GetBoolean(reader.GetOrdinal("pagado")),
                        idTurno = Convert.ToInt32(reader["idTurno"])


                    };
                    cargas.Add(c);
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw new Exception("se produjo un error al obtener las Cargas" + ex.ToString());

            }
            finally
            {
                Comm?.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return cargas;


        }
        public async Task<int> CrearCarga(CargasDTO c)
        {
            using (SqlConnection sqlConnection = conexion())
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand Comm = sqlConnection.CreateCommand())
                {
                    Comm.CommandText = "dbo.InsertarCarga";
                    Comm.CommandType = CommandType.StoredProcedure;

                    Comm.Parameters.Add("@idAguador", SqlDbType.Int).Value = c.idAguador;
                    Comm.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Now;
                    Comm.Parameters.Add("@garrafones", SqlDbType.Int).Value = c.garrafones;
                    Comm.Parameters.Add("@pagado", SqlDbType.Bit).Value = c.pagado;
                    Comm.Parameters.Add("@idTurno", SqlDbType.Int).Value = c.idTurno;

                    var idGenerado = Convert.ToInt32(await Comm.ExecuteScalarAsync());

                    return idGenerado;
                }
            }
        }
        public async Task<CargasDTO> ModificarCarga(int id, CargasDTO c) {

            SqlConnection sqlConnection = conexion();
            SqlCommand Comm = null;

            try
            {
                await sqlConnection.OpenAsync();
                Comm = sqlConnection.CreateCommand();
                Comm.CommandText = "dbo.ModificarCarga";
                Comm.CommandType = CommandType.StoredProcedure;

                Comm.Parameters.Add("@idCarga", SqlDbType.Int).Value = id;
                Comm.Parameters.Add("@idAguador", SqlDbType.Int).Value = c.idAguador;
                Comm.Parameters.Add("@garrafones", SqlDbType.Int).Value = c.garrafones;
                Comm.Parameters.Add("@pagado", SqlDbType.Bit).Value = c.pagado;
                Comm.Parameters.Add("@idTurno", SqlDbType.Int).Value = c.idTurno;
                await Comm.ExecuteNonQueryAsync();


            }
              catch (Exception ex) 
            {
                throw new Exception("se produjo un error al Modificar la Carga" + ex.ToString());
            }
            finally
            {
                Comm?.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return c;
        }
        public async Task<bool> EliminarCarga(int id) {
            SqlConnection sqlConnection = conexion();
            SqlCommand Comm = null;

            try
            {
                await sqlConnection.OpenAsync();
                Comm = sqlConnection.CreateCommand();
                Comm.CommandText = "dbo.EliminarCarga";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@idCarga", SqlDbType.Int).Value = id;
                var resultado = await Comm.ExecuteScalarAsync();
                return Convert.ToInt32(resultado) > 0;
            }
            catch (Exception)
            {
                throw (new Exception());
            }
            finally 
            {
               
                Comm?.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();

            }
         

        }


    }
}
