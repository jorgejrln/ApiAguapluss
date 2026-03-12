namespace ApiAguapluss.DTO_s
{
    public class TurnoDTO
    {
        public DateTime? fecha { get; set; }
        public int idTrabajador { get; set; }
        public int lecIn { get; set; }
        public int lecFin { get; set; }
        public int total { get; set; }
        public int fondo { get; set; }
        public int corte { get; set; }

        public DateTime fechaFin { get; set; }
        public bool activo { get; set; }

    }
}
