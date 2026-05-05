using ConsultaExterna.Dominio;
using ConsultaExterna.DTO;

namespace ConsultaExterna.Mapeadores
{
    public static class MapeadorConsulta
    {
        public static ConsultaDTO ToConsultaDTO(this Consulta consulta)
        {
            return new ConsultaDTO
            {
                Codigo = consulta.Codigo,
                CodigoCita = consulta.CodigoCita,
                Fecha = consulta.Fecha,
                Hora = consulta.Hora,
                MotivoConsulta = consulta.MotivoConsulta,
                Anamenesis = consulta.Anamenesis,
                ExamenFisico = consulta.ExamenFisico,
                PlanTratamiento = consulta.PlanTratamiento
            };
        }
    }
}
