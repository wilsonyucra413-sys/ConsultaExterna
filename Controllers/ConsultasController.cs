using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConsultaExterna.Data;
using ConsultaExterna.Dominio;
using ConsultaExterna.DTO;
using ConsultaExterna.Mapeadores;

namespace ConsultaExterna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private readonly ConsultaExternaContext context;

        public ConsultasController(ConsultaExternaContext context)
        {
            this.context = context;
        }

        // GET: api/Consultas
        [HttpGet("MostrarConsultas")]
        public async Task<ActionResult> GetConsulta()
        {
            return Ok(await context.Consulta
                    .Where(e => e.Estado == "Activo")
                    .Select(e => e.ToConsultaDTO())
                    .ToListAsync());
        }

        [HttpGet("MostrarConsultaDiagnosticos")]
        public async Task<ActionResult> GetConsultaDiagnosticos()
        {
            var pe = await (from c in context.Consulta
                            join d in context.DiagnosticoConsulta
                            on c.IdConsulta equals d.IdConsulta
                            where c.Estado == "Activo" && d.Estado == "Activo"
                            select new ConsultaDiagnosticosDTO
                            {
                                Codigo = c.Codigo,
                                Fecha = c.Fecha,
                                MotivoConsulta = c.MotivoConsulta,
                                Descripcion = d.Descripcion,
                            }).ToListAsync();

            return Ok(pe);
        }
        [HttpGet("ConsultaDiagnosticosTipo")]
        public async Task<ActionResult> GetConsultaDiagnosticosTipo()
        {
            var pe = await (from c in context.Consulta
                                   join d in context.DiagnosticoConsulta
                                   on c.IdConsulta equals d.IdConsulta
                                   join t in context.TipoDiagnostico
                                   on d.IdTipoDiagnostico equals t.IdTipoDiagnostico
                                   where c.Estado == "Activo" && d.Estado == "Activo" && t.Estado == "Activo"
                                   select new ConsultaDiagnosticosTipoDTO
                                   {
                                       Codigo = c.Codigo,
                                       Fecha = c.Fecha,
                                       MotivoConsulta = c.MotivoConsulta,
                                       Descripcion = d.Descripcion,
                                       TipoDiagnostico = t.Nombre
                                   }).ToListAsync();
            return Ok(pe);
        }
        [HttpGet("BuscarConsulta/{codigo}")]
        public async Task<ActionResult> GetConsulta(string codigo)
        {
            var con = await context.Consulta
                .FirstOrDefaultAsync(c => c.Codigo == codigo);

            if (con == null)
                return NotFound(new { mensaje = "Codigo no existe" });
            if (con != null && con.Estado == "Inactivo")
                return NotFound(new { mensaje = "Consulta inactiva" });

            return Ok(new { mensaje = "Consulta encontrada", consulta = con.ToConsultaDTO() });
        }
        [HttpGet("MostrarConsultaDiagnosticos/{codigoconsulta}")]
        public async Task<ActionResult> GetConsultaDiagnosticos(string codigoconsulta)
        {
            var pe = await (from c in context.Consulta
                            join d in context.DiagnosticoConsulta
                            on c.IdConsulta equals d.IdConsulta
                            where c.Codigo == codigoconsulta
                            && c.Estado == "Activo"
                            && d.Estado == "Activo"
                            select new ConsultaDiagnosticosDTO
                            {
                                Codigo = c.Codigo,
                                Fecha = c.Fecha,
                                MotivoConsulta = c.MotivoConsulta,
                                Descripcion = d.Descripcion,
                            }).ToListAsync();

            return Ok(pe);
        }
        [HttpGet("ConsultaDiagnosticosTipo/{codigoconsulta}/{codigotipodiagnostico}")]
        public async Task<ActionResult> GetConsultaDiagnosticosTipo(string codigoconsulta, string codigotipodiagnostico)
        {
            var pe = await (from c in context.Consulta
                            join d in context.DiagnosticoConsulta
                            on c.IdConsulta equals d.IdConsulta
                            join t in context.TipoDiagnostico
                            on d.IdTipoDiagnostico equals t.IdTipoDiagnostico
                            where c.Codigo == codigoconsulta && t.Codigo == codigotipodiagnostico && c.Estado == "Activo" && d.Estado == "Activo" && t.Estado == "Activo"
                            select new ConsultaDiagnosticosTipoDTO
                            {
                                Codigo = c.Codigo,
                                Fecha = c.Fecha,
                                MotivoConsulta = c.MotivoConsulta,
                                Descripcion = d.Descripcion,
                                TipoDiagnostico = t.Nombre
                            }).ToListAsync();
            return Ok(pe);
        }
        // Combinar todos los datos de consulta, diagnostico consulta y tipo diagnostico
        [HttpGet("GeneralConsultaDiagnosticoTipo")]
        public async Task<ActionResult> GetListadoGeneral()
        {
            var datos = await (
                from c in context.Consulta
                join dc in context.DiagnosticoConsulta
                    on c.IdConsulta equals dc.IdConsulta
                join td in context.TipoDiagnostico
                    on dc.IdTipoDiagnostico equals td.IdTipoDiagnostico
                where c.Estado == "Activo"
                && dc.Estado == "Activo"
                && td.Estado == "Activo"
                select new AllConsultaDiagnosticoTipoDTO
                {
                    Codigo= c.Codigo,
                    CodigoCita = c.CodigoCita,
                    Fecha= c.Fecha,
                    Hora = c.Hora,
                    MotivoConsulta = c.MotivoConsulta,
                    Anamenesis = c.Anamenesis,
                    ExamenFisico = c.ExamenFisico,
                    PlanTratamiento = c.PlanTratamiento,
                    TipoDiagnostico = td.Nombre,
                    CodigoFarmacia = dc.CodigoFarmacia,
                    DiagnosticoDescripcion = dc.Descripcion,
                }
            ).ToListAsync();

            return Ok(datos);
        }
        // PUT: api/Consultas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("ActulizarConsulta/{codigo}")]
        public async Task<IActionResult> PutConsulta(string codigo,UpdateConsultaDTO consulta)
        {
            Consulta con = await context.Consulta.FirstOrDefaultAsync(c => c.Codigo == codigo);
            if (con == null) return NotFound(new {mensaje="no existe ese codigo"});
            if (con.Estado == "Inactivo")  return NotFound(new {mensaje="la consulta esta inactiva"});

            con.Fecha = consulta.Fecha;
            con.Hora = consulta.Hora;
            con.MotivoConsulta = consulta.MotivoConsulta;
            con.Anamenesis = consulta.Anamenesis;
            con.ExamenFisico = consulta.ExamenFisico;
            con.PlanTratamiento = consulta.PlanTratamiento;
            await context.SaveChangesAsync();
            return Ok(new { mensaje = "Consulta actualizada correctamente", consulta=con.ToConsultaDTO() });
        }

        // POST: api/Consultas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("CrearConsulta/{codigo}")]
        public async Task<ActionResult> PostConsulta(string codigo ,CreateConsultaDTO consulta)
        {
            var pe = await context.Consulta.FirstOrDefaultAsync(c => c.Codigo == codigo);
            if(pe != null && pe.Estado == "Activo") return BadRequest(new { mensaje = "El código ya existe" });
            if(pe != null && pe.Estado == "Inactivo") return BadRequest(new { mensaje = "El código ya existe pero esta inactivo" });
            Consulta con = new Consulta
            {
                CodigoCita=consulta.CodigoCita,
                Codigo = codigo,
                Fecha = consulta.Fecha,
                Hora = consulta.Hora,
                MotivoConsulta = consulta.MotivoConsulta,
                Anamenesis = consulta.Anamenesis,
                ExamenFisico = consulta.ExamenFisico,
                PlanTratamiento = consulta.PlanTratamiento,
                Estado = "Activo"
            };
            await context.Consulta.AddAsync(con);
            await context.SaveChangesAsync();
            return Ok(new {mensaje = "Se registro nueva consulta", consulta=con.ToConsultaDTO()});
        }

        // DELETE: api/Consultas/5
        [HttpDelete("BorrarConsulta/{codigo}")]
        public async Task<IActionResult> DeleteConsulta(string codigo)
        {
            Consulta consulta = await context.Consulta.FirstOrDefaultAsync(c => c.Codigo == codigo);
            if (consulta == null) return BadRequest(new { mensaje = "No existe ese codigo" });
            if (consulta.Estado == "Inactivo") return BadRequest(new { mensaje = "La consulta ya esta inactiva" });

            consulta.Estado = "Inactivo";
            await context.DiagnosticoConsulta
                .Where(d => d.IdConsulta == consulta.IdConsulta)
                .ExecuteUpdateAsync(e => e
                .SetProperty(d => d.Estado, "Inactivo"));
            await context.NotaEvaluacion
                .Where(d => d.IdConsulta == consulta.IdConsulta)
                .ExecuteUpdateAsync(e => e
                .SetProperty(d => d.Estado, "Inactivo"));
            await context.SaveChangesAsync();
            return Ok(new { mensaje = "Se elimino el dato" ,consulta=consulta.ToConsultaDTO()});
        }
        
        
    }
}
