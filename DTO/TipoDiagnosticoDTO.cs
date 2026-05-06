namespace ConsultaExterna.DTO
{
    public class TipoDiagnosticoDTO
    {
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

    }
    public class UpdateTipoDiagnosticoDTO
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
    public class CreateTipoDiagnosticoDTO
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}
