using System.ComponentModel.DataAnnotations;

namespace ConsultaExterna.DTO
{
    public class DiagnosticoClinicoDTO
    {
        [Key]
        public int ExamenId { get; set; }
        public string ExamenCodigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int TiempoProcesamiento { get; set; }
        public bool RequiereAyuno { get; set; }
        public string Estado { get; set; }
    }
    public class DiagnosticoClinicoGet
    {
        public string ExamenCodigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int TiempoProcesamiento { get; set; }
    }
}
