namespace ApiAguapluss.repositorio
{
    public class AcessoSQL
    {
        public string CadenaConexionSQL { get; }

        public AcessoSQL(IConfiguration configuration)
        {
            CadenaConexionSQL = configuration.GetConnectionString("SQL");
        }
    }
}
