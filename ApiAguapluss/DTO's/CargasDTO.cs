namespace ApiAguapluss.DTO_s
{
    public class CargasDTO
    {
      

        public DateTime? fecha { get; init; }

        public int idAguador { get; set; }
        public int garrafones { get; set; }

        public bool pagado { get; set; }

        public int idTurno { get; init; }

    }
}
