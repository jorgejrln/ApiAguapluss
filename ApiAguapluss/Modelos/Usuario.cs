namespace ApiAguapluss.Modelos
{
    public class Usuario
    {
        public int idUsuario { get; set; }

        public string usuario { get; set; }
        public string password { get; set; }    
        public int idRol{ get; set; }
        public int idTrabajador { get; set; }

        public string rol { get; set; }
    }
}
