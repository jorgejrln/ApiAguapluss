using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ApiAguapluss.DTO_s;
using ApiAguapluss.Modelos;

namespace ApiAguapluss.repositorio
{
    public class AguadoresSQL
    {
        private String CadenaConexion;
        public AguadoresSQL(AcessoSQL cadenaConexion)
        {
            CadenaConexion = cadenaConexion.CadenaConexionSQL;
        }
        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);

        }


        public async Task<IEnumerable<Aguadores>> DameTrabajadores()
        {
            SqlConnection sqlConnection = conexion();
            SqlCommand Comm = null;
            Aguadores a = null;
            List<Aguadores> aguador = new List<Aguadores>();

            try 
            {
                await sqlConnection.OpenAsync();
                Comm = sqlConnection.CreateCommand();
                Comm.CommandText = "dbo.ObtenerAguadores";
                Comm.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();

                while (reader.Read()) {
                    a = new Aguadores()
                    {
                        idAguador = Convert.ToInt32(reader["idAguador"]),
                        nombreAguador = reader["NombreAguador"].ToString(),
                        apellidoAguador = reader["apellidoAguador"].ToString(),
                        telefonoAguador = reader["TelefonoAguador"].ToString(),
                        fotoAguador = reader["FotoURlAguador"].ToString(),

                    };
                    aguador.Add(a);
                }
                return aguador;
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
        public async Task<int> CrearTrabajador(AguadorDTO a)
        {
            using (SqlConnection sqlConnection = conexion())
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand Comm = sqlConnection.CreateCommand())
                {
                    Comm.CommandText = "dbo.CrearAguador";
                    Comm.CommandType = CommandType.StoredProcedure;

                    Comm.Parameters.Add("@nombreAguador", SqlDbType.VarChar).Value = a.nombreAguador;
                    Comm.Parameters.Add("@apellidoAguador", SqlDbType.VarChar).Value =a.apellidoAguador;
                    Comm.Parameters.Add("@telefonoAguador", SqlDbType.VarChar).Value = a.telefonoAguador;
                    Comm.Parameters.Add("@fotoAguador", SqlDbType.VarChar).Value = a.fotoAguador;


                    var idGenerado = Convert.ToInt32(await Comm.ExecuteScalarAsync());

                    return idGenerado;
                }
            }

        }
        public async Task<AguadorDTO> ModificarAguador(int id, AguadorDTO a)
        {

            SqlConnection sqlConnection = conexion();
            SqlCommand Comm = null;
            try
            {
                await sqlConnection.OpenAsync();
                Comm = sqlConnection.CreateCommand();
                Comm.CommandText = "dbo.ModificarAguador";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@idAguador", SqlDbType.Int).Value = id;
                Comm.Parameters.Add("@nombreAguador", SqlDbType.VarChar).Value = a.nombreAguador;
                Comm.Parameters.Add("@apellidoAguador", SqlDbType.VarChar).Value = a.apellidoAguador;
                Comm.Parameters.Add("@telefonoAguador", SqlDbType.VarChar).Value = a.telefonoAguador;
                Comm.Parameters.Add("@fotoAguador", SqlDbType.NVarChar).Value = a.fotoAguador;
                await Comm.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("se produjo un error al Modificar el aguador" + ex.ToString());
            }
            finally
            {
                Comm?.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return a;
        }



    }
}
