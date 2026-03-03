using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using ApiAguapluss.DTO_s;
using ApiAguapluss.Modelos;

namespace ApiAguapluss.repositorio
{
    public class TrabajadoresSQL
    {
        private String CadenaConexion;
        public TrabajadoresSQL(AcessoSQL cadenaConexion)
        {
            CadenaConexion = cadenaConexion.CadenaConexionSQL;
        }
        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);


        }

        public async Task<IEnumerable<Trabajador>> DameTrabajadores() 
        {
            SqlConnection sqlConnection = conexion();
            SqlCommand Comm = null;
            Trabajador t = null;
            List<Trabajador>trabajador = new List<Trabajador>();
            try 
            {
                await sqlConnection.OpenAsync();
                Comm = sqlConnection.CreateCommand();
                Comm.CommandText = "dbo.ObtenerTrabajadores";
                Comm.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();

                while (await reader.ReadAsync()) 
                {
                    t = new Trabajador()
                    {
                        idTrabajador = Convert.ToInt32(reader["idTrabajador"]),

                        nombreTrabajador = reader["NombreTrabajador"].ToString(),
                        apellidoTrabajador = reader["ApellidoTrabajador"].ToString(),
                        telefonoTrabajador = reader["TelefonoTrabajador"].ToString(),
                        Foto = reader["FotoUrl"] as string,



                    };
                    trabajador.Add(t);
                }
                return trabajador;
            } 
            catch (Exception ex) 
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
        public async Task<int> CrearTrabajador(TrabajadorDTO t )
        {
            using (SqlConnection sqlConnection = conexion())
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand Comm = sqlConnection.CreateCommand())
                {
                    Comm.CommandText = "dbo.CrearTrabajador";
                    Comm.CommandType = CommandType.StoredProcedure;

                    Comm.Parameters.Add("@nombreTrabajador", SqlDbType.VarChar).Value = t.nombreTrabajador;
                    Comm.Parameters.Add("@apellidoTrabajador", SqlDbType.VarChar).Value = t.apellidoTrabajador;
                    Comm.Parameters.Add("@telefono", SqlDbType.VarChar).Value = t.telefonoTrabajador;
                    Comm.Parameters.Add("@FotoUrl", SqlDbType.VarChar).Value = t.Foto;
                    

                    var idGenerado = Convert.ToInt32(await Comm.ExecuteScalarAsync());

                    return idGenerado;
                }
            }

        }

        public async Task<TrabajadorDTO> ModificiarTrabajador(int id, TrabajadorDTO t) 
        {
            SqlConnection sqlConnection = conexion();
            SqlCommand Comm = null;
            try
            {
                await sqlConnection.OpenAsync();
                Comm = sqlConnection.CreateCommand();
                Comm.CommandText = "dbo.ModificarTrabajador";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@idTrabajador", SqlDbType.Int).Value = id;
                Comm.Parameters.Add("@nombreTrabajador", SqlDbType.VarChar).Value = t.nombreTrabajador;
                Comm.Parameters.Add("@apellidoTrabajador", SqlDbType.VarChar).Value = t.apellidoTrabajador;
                Comm.Parameters.Add("@telefono", SqlDbType.VarChar).Value = t.telefonoTrabajador;
                Comm.Parameters.Add("@FotoUrl", SqlDbType.NVarChar).Value = t.Foto;
                await Comm.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("se produjo un error al Modificar el trabajador" + ex.ToString());
            }
            finally
            {
                Comm?.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return t;
        }






    }
}
