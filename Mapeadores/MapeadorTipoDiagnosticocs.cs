using ConsultaExterna.Dominio;
using ConsultaExterna.DTO;

namespace ConsultaExterna.Mapeadores
{
    public static class MapeadorTipoDiagnosticocs
    {
        public static TipoDiagnosticoDTO ToTipoDiagnosticoDTO(this TipoDiagnostico tipoDiagnostico)
            {
                return new TipoDiagnosticoDTO
                {
                    Codigo = tipoDiagnostico.Codigo,
                    Nombre = tipoDiagnostico.Nombre,
                    Descripcion = tipoDiagnostico.Descripcion
                };
        }
    }
}
