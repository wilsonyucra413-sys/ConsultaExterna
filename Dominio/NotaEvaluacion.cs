using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConsultaExterna.Dominio
{
    public class NotaEvaluacion
    {
        [Key]
        public int IdNotaEvaluacion { get; set; }
        public DateOnly Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public string Estado { get; set; }
        public int IdConsulta { get; set; }    
        [ForeignKey("IdConsulta")]
        [JsonIgnore]
        public Consulta Consulta { get; set; }
    }
}
