namespace ConsultaExterna.DTO
{
    public class ConsultaDTO
    {
        public string Codigo { get; set; }
        public string CodigoCita { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public string MotivoConsulta { get; set; }
        public string Anamenesis { get; set; }
        public string ExamenFisico { get; set; }
        public string PlanTratamiento { get; set; }
    }
    public class UpdateConsultaDTO
    {
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public string MotivoConsulta { get; set; }
        public string Anamenesis { get; set; }
        public string ExamenFisico { get; set; }
        public string PlanTratamiento { get; set; }
    }
    public class CreateConsultaDTO
    {
        public string CodigoCita { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public string MotivoConsulta { get; set; }
        public string Anamenesis { get; set; }
        public string ExamenFisico { get; set; }
        public string PlanTratamiento { get; set; }
    }
    public class ConsultaDiagnosticosDTO
    {
        public string Codigo { get; set; }
        public DateOnly Fecha { get; set; }
        public string MotivoConsulta { get; set; }
        public string Descripcion { get; set; }
    }
    public class AllConsultaDiagnosticoTipoDTO
    {
        public string Codigo { get; set; }
        public string CodigoCita { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public string MotivoConsulta { get; set; }
        public string Anamenesis { get; set; }
        public string ExamenFisico { get; set; }
        public string PlanTratamiento { get; set; }
        public string TipoDiagnostico { get; set; }
        public string CodigoFarmacia { get; set; }
        public string DiagnosticoDescripcion { get; set; }
    }
    public class WithoutDiagnosticoDTO 
    {
        public string Codigo { get; set; }
        public string CodigoCita { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public string MotivoConsulta { get; set; } 
    }
    public class ConsultaDiagnosticosTipoDTO 
    {
        public string Codigo { get; set; }
        public DateOnly Fecha { get; set; }
        public string MotivoConsulta { get; set; }
        public string Descripcion { get; set; }
        public string TipoDiagnostico { get; set; }
    }
}
