using ConsultaExterna.Data;
using ConsultaExterna.Dominio;
using ConsultaExterna.DTO;
using ConsultaExterna.Mapeadores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaExterna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosticoConsultasController : ControllerBase
    {
        private readonly ConsultaExternaContext context;

        public DiagnosticoConsultasController(ConsultaExternaContext context)
        {
            this.context = context;
        }

        // GET: api/DiagnosticoConsultas
        [HttpGet("MostrarDiagnostico")]
        public async Task<ActionResult> GetDiagnosticoConsulta()
        {
            var dig= await context.DiagnosticoConsulta
                .Include(d => d.Consulta) 
                .Include(d => d.TipoDiagnostico)
                .Where(d => d.Estado == "Activo")
                .Select(d => d.ToDiagnosticoConsultaDTO())
                .ToListAsync();
            //o es mejor asi ?
            //var dig = await context.DiagnosticoConsulta
            //.Where(d => d.Estado == "Activo")
            //.Select(d => new DiagnosticoConsultaDTO
            //{
            //    CodigoConsulta = d.Consulta.Codigo,
            //    CodigoFarmacia = d.CodigoFarmacia,
            //    NombreTipo = d.TipoDiagnostico.Nombre,
            //    Descripcion = d.Descripcion
            //})
            //.ToListAsync();
            return Ok(dig);
        }
        [HttpGet("BuscarDiagnosticoConsulta/{codigodiagnosticoconsulta}")]
        public async Task<IActionResult> GetDiagnosticoConsulta(string codigodiagnosticoconsulta)
        {
            var pe = await context.DiagnosticoConsulta
                .Include(d => d.Consulta)
                .Include(d => d.TipoDiagnostico)
                .FirstOrDefaultAsync(p => p.Codigo==codigodiagnosticoconsulta);
            if (pe == null) return NotFound(new {mensaje = "No existe ese codigo"});
            if (pe.Estado == "Inactivo")
            {
                return NotFound(new {mensaje = "El codigo esta inactivo"});
            }
            return Ok(pe.ToDiagnosticoConsultaDTO());
        }
        //mostrar la cantidad de diagnosticos por tipo diagnostico
        [HttpGet("CantidadDiagnosticos")]
        public async Task<ActionResult> GetCantidadDiagnosticos()
        {
            var datos = await (
                from dc in context.DiagnosticoConsulta
                join td in context.TipoDiagnostico
                    on dc.IdTipoDiagnostico equals td.IdTipoDiagnostico
                where dc.Estado == "Activo"
                && td.Estado == "Activo"
                group dc by td.Nombre into grupo
                select new
                {
                    TipoDiagnostico = grupo.Key,
                    Cantidad = grupo.Count()
                }
            ).ToListAsync();

            return Ok(datos);
        }
        [HttpGet("SumaDiagnosticos")]
        public async Task<ActionResult> GetSumaDiagnosticos()
        {
            var datos = await (
                from dc in context.DiagnosticoConsulta
                join td in context.TipoDiagnostico
                    on dc.IdTipoDiagnostico equals td.IdTipoDiagnostico
                where dc.Estado == "Activo"
                && td.Estado == "Activo"
                group dc by td.Nombre into grupo
                select new
                {
                    TipoDiagnostico = grupo.Key,
                    Total = grupo.Sum(x => x.IdDiagnosticoConsulta)
                }
            ).ToListAsync();

            return Ok(datos);
        }

        [HttpGet("SinDiagnostico")]
        public async Task<ActionResult> GetConsultasSinDiagnostico()
        {
            var datos = await (
                from c in context.Consulta
                where c.Estado == "Activo"
                && !context.DiagnosticoConsulta
                    .Any(dc => dc.IdConsulta == c.IdConsulta
                            && dc.Estado == "Activo")
                select new WithoutDiagnosticoDTO
                {
                    Codigo = c.Codigo,
                    CodigoCita = c.CodigoCita,
                    Fecha = c.Fecha,
                    Hora = c.Hora,
                    MotivoConsulta = c.MotivoConsulta
                }
            ).ToListAsync();

            return Ok(datos);
        }
        //PUT: api/DiagnosticoConsultas/5
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("ActualizarDiagnosticoConsulta/{codigodiagnosticonsulta}")]
        public async Task<IActionResult> PutDiagnosticoConsulta(string codigodiagnosticonsulta,UpdateDiagnosticoConsultaDTO updig)
        {
            var dig = await context.DiagnosticoConsulta
                .Include(d => d.Consulta)
                .Include(d => d.TipoDiagnostico)
                .FirstOrDefaultAsync(c => c.Codigo == codigodiagnosticonsulta);
            if (dig == null) return BadRequest(new {mensaje="No existe ese diagnostico consulta"});
            if(dig.Estado == "Inactivo") return BadRequest(new { mensaje = "El diagnostico consulta esta inactivo" });
            dig.Descripcion = updig.Descripcion;
            await context.SaveChangesAsync();
            return Ok(new {mensaje = "Se actualizo el diagnostico consulta",digConsulta= dig.ToDiagnosticoConsultaDTO()});
        }

        // POST: api/DiagnosticoConsultas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("CrearConsultaDiagnostico/{codigo}")]
        public async Task<ActionResult> PostDiagnosticoConsulta(string codigo,CreateDiagnosticoConsultaDTO crdig)
        {
            var pe = await context.DiagnosticoConsulta
        .FirstOrDefaultAsync(p => p.Codigo == codigo);

            if (pe != null && pe.Estado == "Activo")
                return BadRequest(new { mensaje = "El diagnostico consulta ya existe" });

            if (pe != null && pe.Estado == "Inactivo")
                return BadRequest(new { mensaje = "El diagnostico consulta esta inactivo" });

            var a = await context.Consulta
                .FirstOrDefaultAsync(c => c.Codigo == crdig.CodigoConsulta);

            if (a == null)
                return BadRequest(new { mensaje = "No existe ese codigo consulta" });

            var b = await context.TipoDiagnostico
                .FirstOrDefaultAsync(t => t.Codigo == crdig.CodigoTipo); 

            if (b == null)
                return BadRequest(new { mensaje = "No existe ese codigo tipo diagnostico" });

            DiagnosticoConsulta nv = new DiagnosticoConsulta
            {
                Codigo = codigo,
                CodigoFarmacia = crdig.CodigoFarmacia,
                IdConsulta = a.IdConsulta,
                IdTipoDiagnostico = b.IdTipoDiagnostico,
                Descripcion = crdig.Descripcion,
                Estado = "Activo",
                Consulta = a,
                TipoDiagnostico = b
            };

            await context.DiagnosticoConsulta.AddAsync(nv);
            await context.SaveChangesAsync();

            var res = await context.DiagnosticoConsulta
                .Include(d => d.Consulta)
                .Include(d => d.TipoDiagnostico)
                .FirstOrDefaultAsync(d => d.Codigo == codigo);

            return Ok(new
            {
                mensaje = "Se creo correctamente un diagnostico consulta",
                Diag = res.ToDiagnosticoConsultaDTO()
            });
        }

        // DELETE: api/DiagnosticoConsultas/5
        [HttpDelete("BorrarDiagnosticoConsulta/{codigoconsulta}")]
        public async Task<IActionResult> DeleteDiagnosticoConsulta(string codigoconsulta)
        {
            var dig = await context.DiagnosticoConsulta
                .Include(d => d.Consulta)
                .Include(d => d.TipoDiagnostico)
                .FirstOrDefaultAsync(p => p.Codigo == codigoconsulta);

            if (dig == null)
                return NotFound(new { mensaje = "No existe ese codigo diagnostico consulta" });

            if (dig.Estado == "Inactivo")
                return BadRequest(new { mensaje = "El diagnostico consulta ya esta inactivo" });

            dig.Estado = "Inactivo";

            await context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Se inactivó el diagnostico consulta correctamente",
                digconsulta = dig.ToDiagnosticoConsultaDTO()
            });
        }
    }
}
