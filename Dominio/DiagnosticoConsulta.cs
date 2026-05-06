using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConsultaExterna.Dominio
{
    public class DiagnosticoConsulta
    {
        [Key]
        public int IdDiagnosticoConsulta { get; set; }
        //el codigo de farmacia me sirve para identificar el tipo de emfermedad
        public string? CodigoFarmacia { get; set; }
        public string? Descripcion { get; set; }
        public string? Codigo { get; set; }
        public string? Estado { get; set; }
        public int IdConsulta { get; set; }
        public int IdTipoDiagnostico { get; set; }
        [ForeignKey("IdConsulta")]
        [JsonIgnore]
        public Consulta? Consulta { get; set; }
        [ForeignKey("IdTipoDiagnostico")]
        [JsonIgnore]
        public TipoDiagnostico? TipoDiagnostico { get; set; }
    }
}
