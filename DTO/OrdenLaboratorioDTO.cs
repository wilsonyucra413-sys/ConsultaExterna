using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaExterna.DTO
{
    public class OrdenLaboratorioDTO
    {
        public string? Code { get; set; } // Corresponde a OrdenLaboratorioCodigo
        public string? PacienteCodigo { get; set; }
        public string? MedicoCodigo { get; set; }
        public DateTime FechaOrden { get; set; }
        public string? TipoAtencion { get; set; }
        public string? Observaciones { get; set; }
    }
}