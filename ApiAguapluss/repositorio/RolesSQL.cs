using System.Data;
using System.Data.SqlClient;
using ApiAguapluss.DTO_s;
using ApiAguapluss.Modelos;
using Microsoft.AspNetCore.Identity;

namespace ApiAguapluss.repositorio
{
    public class RolesSQL
    {
        private String CadenaConexion;
        public RolesSQL(AcessoSQL cadenaConexion)
        {
            CadenaConexion = cadenaConexion.CadenaConexionSQL;
        }
        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);


        }


        public async Task<IEnumerable<Rol>> DameRoles()
        {

            SqlConnection sqlConnection = conexion();
            SqlCommand Comm = null;
            Rol r = null;
            List<Rol> roles = new List<Rol>();

            try
            {
                await sqlConnection.OpenAsync();
                Comm = sqlConnection.CreateCommand();
                Comm.CommandText = "dbo.ObtenerRoles";
                Comm.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();



                while (await reader.ReadAsync())
                {
                    r = new Rol()
                    {

                        idRol = Convert.ToInt32(reader["idRol"]),
                        rol = reader["nombreRol"].ToString(),
                    }; 
                   
                   roles.Add(r);
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
            return roles;
        }

        public async Task<int>insertaRol(RolDTO r)
        {

            using (SqlConnection sqlConnection = conexion())
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand Comm = sqlConnection.CreateCommand())
                {
                    Comm.CommandText = "dbo.InsertarRol";
                    Comm.CommandType = CommandType.StoredProcedure;

                    Comm.Parameters.Add("@nombreRol", SqlDbType.VarChar).Value = r.rol;
                    

                    var idGenerado = Convert.ToInt32(await Comm.ExecuteScalarAsync());

                    return idGenerado;
                }


            }





        }


    }
}
