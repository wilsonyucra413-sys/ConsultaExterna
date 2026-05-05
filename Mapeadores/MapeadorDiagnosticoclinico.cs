using ConsultaExterna.DTO;

namespace ConsultaExterna.Mapeadores
{
    public static class MapeadorDiagnosticoclinico
    {
        public static DiagnosticoClinicoGet ToDiagnosticoDTO(this DiagnosticoClinicoDTO examen)
        {
            return new DiagnosticoClinicoGet
            {
                ExamenCodigo = examen.ExamenCodigo,
                Nombre = examen.Nombre,
                Descripcion = examen.Descripcion,
                TiempoProcesamiento = examen.TiempoProcesamiento
            };
        }
    }
}
