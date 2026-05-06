namespace ConsultaExterna.DTO
{
    public class DiagnosticoConsultaDTO
    {
        public string? Codigo { get; set; }
        public string? CodigoConsulta { get; set; }
        public string? CodigoFarmacia { get; set; }
        public string? CodigoTipo { get; set; }
        public string? Descripcion { get; set; }

    }
    public class UpdateDiagnosticoConsultaDTO
    {
        public string? CodigoConsulta { get; set; }
        public string? CodigoFarmacia { get; set; }
        public string? CodigoTipo { get; set; }
        public string? Descripcion { get; set; }
    }
    public class CreateDiagnosticoConsultaDTO
    {
        public string? CodigoConsulta { get; set; }
        public string? CodigoFarmacia { get; set; }
        public string? CodigoTipo { get; set; }
        public string? Descripcion { get; set; }
    }
}
