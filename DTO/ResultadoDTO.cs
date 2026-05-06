using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaExterna.DTO
{
    public class ResultadoDTO
    {
        public string ResultadoCodigo { get; set; }
        public string CodigoPaciente { get; set; }
        public string OrdenCodigo { get; set; }
        public string TipoAtencion { get; set; }
        public string ExamenNombre { get; set; }
        public string ParametroNombre { get; set; }
        public decimal Valor { get; set; }
        public string Unidad { get; set; }
        public string Referencia { get; set; }
        public DateOnly Fecha { get; set; }
    }
    public class ResultadoResponseDTO
    {
        public string Mensaje { get; set; }
        public int TotalParametros { get; set; }
        public List<ResultadoDTO> Data { get; set; }
    }
    public class ResultadoSimpleDTO
    {
        public string ResultadoCodigo { get; set; }
        public string MuestraCodigo { get; set; }
        public string ParametroExamenCodigo { get; set; }
        public decimal Valor { get; set; }
        public string EquipoCodigo { get; set; }
        public DateOnly FechaResultado { get; set; }
    }
    public class ResultadoListResponseDTO
    {
        public string Mensaje { get; set; }
        public List<ResultadoSimpleDTO> Resultados { get; set; } // Debe llamarse igual al JSON: "resultados"
    }
}