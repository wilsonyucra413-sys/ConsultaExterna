namespace ConsultaExterna.DTO
{
    public class GestionCitasTurnosDTO
    {
        public string? CodigoPaciente { get; set; }
        public string? CodigoCita { get; set; }
        public string? CodigoTurno { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
    }
    public class AllGestionCitasTurnosDTO
    {
            public string? CodigoPaciente { get; set; }
            public string? CodigoCita { get; set; }
            public string? CodigoTurno { get; set; }
            public string? Fecha { get; set; }
            public string? Hora { get; set; }
            public string? Estado { get; set; }
    }

}
