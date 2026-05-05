using ConsultaExterna.Dominio;
using ConsultaExterna.DTO;

namespace ConsultaExterna.Mapeadores
{
    public static class MapeadorDiagnosticoConsulta
    {
            public static DiagnosticoConsultaDTO ToDiagnosticoConsultaDTO(this DiagnosticoConsulta diagnosticoConsulta)
            {
                return new DiagnosticoConsultaDTO
                {
                    Codigo = diagnosticoConsulta.Codigo,
                    CodigoConsulta = diagnosticoConsulta.Consulta.Codigo,
                    CodigoFarmacia = diagnosticoConsulta.CodigoFarmacia,
                    CodigoTipo = diagnosticoConsulta.TipoDiagnostico.Codigo,
                    Descripcion = diagnosticoConsulta.Descripcion
                };
        }
    }
}
