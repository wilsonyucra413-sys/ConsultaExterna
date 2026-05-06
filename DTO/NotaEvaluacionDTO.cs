namespace ConsultaExterna.DTO
{
    public class NotaEvaluacionDTO
    {
        public string? CodigoConsulta { get; set; }
        public string? Codigo { get; set; }
        public DateOnly Fecha { get; set; }
        public string? Descripcion { get; set; } 
    }
    public class UpdateNotaEvaluacionDTO
    {
        public DateOnly Fecha { get; set; }
        public string? Descripcion { get; set; }
    }
    public class CreateNotaEvaluacionDTO
    {
        public DateOnly Fecha { get; set; }
        public string? Descripcion { get; set; }
        public string? CodigoConsulta { get; set; }
    }
}
