using System.ComponentModel.DataAnnotations;

namespace ConsultaExterna.Dominio
{
    public class Consulta
    {
        [Key]
        public int IdConsulta { get; set; }
        public string CodigoCita { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public string MotivoConsulta { get; set; }
        public string Anamenesis { get; set; }
        public string ExamenFisico { get; set; }
        public string PlanTratamiento { get; set; }
        public string Codigo { get; set; }
        public string Estado { get; set; }
        public List<DiagnosticoConsulta> DiagnosticoConsulta { get; set; }
        public List<NotaEvaluacion> NotaEvaluacion { get; set; }

    }
}
