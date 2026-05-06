using System.ComponentModel.DataAnnotations;

namespace ConsultaExterna.Dominio
{
    public class TipoDiagnostico
    {
        [Key]
        public int IdTipoDiagnostico { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Codigo { get; set; }
        public string? Estado { get; set; }
        public List<DiagnosticoConsulta>? DiagnosticoConsulta { get; set; }

    }
}
