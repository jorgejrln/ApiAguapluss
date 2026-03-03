namespace ApiAguapluss.Modelos
{
    public class Carga
    {
        public int idCarga { get; init; }

        public DateTime? fecha { get; init; }

        public int idAguador { get; set; }
        public int garrafones { get; set; }

        public bool pagado { get; set; }

        public int idTurno { get; init; }

    }
}
