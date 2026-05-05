using ConsultaExterna.Dominio;
using ConsultaExterna.DTO;

namespace ConsultaExterna.Mapeadores
{
    public static class MapeadorNotaEvaluacion
    {
        public static NotaEvaluacionDTO ToNotaEvaluacionDTO(this NotaEvaluacion nota)
        {
            return new NotaEvaluacionDTO   
            {
                Fecha = nota.Fecha,
                Descripcion = nota.Descripcion,
                Codigo = nota.Codigo,
                CodigoConsulta= nota.Consulta.Codigo,
            };
        }
    }
}
