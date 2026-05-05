using ConsultaExterna.DTO;

namespace ConsultaExterna.Mapeadores
{
    public static class MapeadorGestionCitasTurnos
    {
        public static GestionCitasTurnosDTO ToGestionCitasTurnosDTO(this GestionCitasTurnosDTO gestion)
        {
            return new GestionCitasTurnosDTO
            {
                CodigoPaciente = gestion.CodigoPaciente,
                CodigoCita = gestion.CodigoCita,
                CodigoTurno = gestion.CodigoTurno,
                Fecha = gestion.Fecha,
                Hora = gestion.Hora,
            };
        }
    }
}
