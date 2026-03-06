using System.Data;
using System.Data.SqlClient;
using ApiAguapluss.DTO_s;
using ApiAguapluss.Modelos;

namespace ApiAguapluss.repositorio
{
    public class UsuariosSQL
    {

        private String CadenaConexion;
        public UsuariosSQL(AcessoSQL cadenaConexion)
        {
            CadenaConexion = cadenaConexion.CadenaConexionSQL;
        }
        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);

        }

        public async Task<int> insertaUsuario(UsuarioDTO u)
        {
            using (SqlConnection sqlConnection = conexion())
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand Comm = sqlConnection.CreateCommand())
                {
                  
                    Comm.CommandText = "dbo.InsertarUsuario";
                    Comm.CommandType = CommandType.StoredProcedure;
                    Comm.Parameters.Add("@usuario", SqlDbType.VarChar).Value = u.usuario;
                    Comm.Parameters.Add("@password", SqlDbType.VarChar).Value = u.password;
                    Comm.Parameters.Add("@idRol", SqlDbType.Int).Value = u.idRol;
                    Comm.Parameters.Add("@idTrabajador", SqlDbType.Int).Value = u.idTrabajador;

                    var idGenerado = Convert.ToInt32(await Comm.ExecuteScalarAsync());

                    return idGenerado;
                }


            }
        }

        public async Task<IEnumerable<Usuario>> dameUsuarios()
        {

            SqlConnection sqlConnection = conexion();
            SqlCommand Comm = null;
            Usuario u = null;
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                await sqlConnection.OpenAsync();
                Comm = sqlConnection.CreateCommand();
                Comm.CommandText = "dbo.ObtenerUsuarios";
                Comm.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();



                while (await reader.ReadAsync())
                {
                    u = new Usuario()
                    {
                        idUsuario = Convert.ToInt32(reader["idUsuario"]),
                        usuario = reader["usuario"].ToString(),
                        password = reader["password"].ToString(),
                        idTrabajador = Convert.ToInt32(reader["idTrabajador"]),
                        idRol = Convert.ToInt32(reader["idRol"]),
                        rol = reader["nombreRol"].ToString()
                    };

                    usuarios.Add(u);
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
            return usuarios;

        }

    }
}
