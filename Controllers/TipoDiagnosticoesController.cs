using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConsultaExterna.Data;
using ConsultaExterna.Dominio;
using ConsultaExterna.Mapeadores;
using ConsultaExterna.DTO;

namespace ConsultaExterna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDiagnosticoesController : ControllerBase
    {
        private readonly ConsultaExternaContext context;

        public TipoDiagnosticoesController(ConsultaExternaContext context)
        {
            this.context = context;
        }

        // GET: api/TipoDiagnosticoes
        [HttpGet("MostrarTipoDiagnostico")]
        public async Task<ActionResult> GetTipoDiagnostico()
        {
            return Ok(await context.TipoDiagnostico
                    .Where(e => e.Estado == "Activo")
                    .Select(p => p.ToTipoDiagnosticoDTO())
                    .ToListAsync());
        }

        // GET: api/TipoDiagnosticoes/5
        [HttpGet("BuscarTipoDiagnostico/{codigo}")]
        public async Task<ActionResult> GetTipoDiagnostico(string codigo)
        {
            var pe = await context.TipoDiagnostico
                    .FirstOrDefaultAsync(p => p.Codigo==codigo);
            if (pe == null)
            {
                return NotFound("No existe ese codigo");
            }
            if ( pe.Estado == "Inactivo") return NotFound("Ese codigo esta inactivo");
            return Ok(pe.ToTipoDiagnosticoDTO());
        }

        // PUT: api/TipoDiagnosticoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("ActualizarTipoDiagnostico/{codigo}")]
        public async Task<IActionResult> PutTipoDiagnostico(string codigo,UpdateTipoDiagnosticoDTO uptd)
        {
            var pe = await context.TipoDiagnostico
                    .FirstOrDefaultAsync(p => p.Codigo == codigo);
            if(pe == null) return NotFound("No existe ese codigo");
             if (pe.Estado == "Inactivo") return NotFound("Ese codigo esta inactivo");
            pe.Descripcion = uptd.Descripcion;
            pe.Nombre = uptd.Nombre;
            await context.SaveChangesAsync();
            return Ok("Se actulizon un nuevo dato");
        }

        // POST: api/TipoDiagnosticoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("CrearTipoDiagnostico/{codigo}")]
        public async Task<ActionResult> PostTipoDiagnostico(string codigo ,CreateTipoDiagnosticoDTO crtd)
        {
            var pe = await context.TipoDiagnostico
                    .FirstOrDefaultAsync(p => p.Codigo == codigo);
            if(pe != null && pe.Estado=="Activo") return NotFound("Ya existe ese codigo");
            if (pe != null && pe.Estado == "Inactivo") return NotFound("Ese codigo esta inactivo");
            var a = new TipoDiagnostico
            {
                Codigo = codigo,
                Nombre = crtd.Nombre,
                Descripcion = crtd.Descripcion,
                Estado = "Activo"
            };
            await context.TipoDiagnostico.AddAsync(a);
            await context.SaveChangesAsync();
            return Ok(a.ToTipoDiagnosticoDTO());
        }

        // DELETE: api/TipoDiagnosticoes/5
        [HttpDelete("BorrarTipoDiagnostico/{codigo}")]
        public async Task<IActionResult> DeleteTipoDiagnostico(string codigo)
        {
            var pe= await context.TipoDiagnostico
                    .FirstOrDefaultAsync(p => p.Codigo == codigo);
            if(pe==null) return NotFound("No existe ese codigo");
            if (pe.Estado == "Inactivo") return NotFound("Ese codigo esta inactivo");
            pe.Estado = "Inactivo";
            await context.SaveChangesAsync();
            return Ok(pe.ToTipoDiagnosticoDTO());
        }
    }
}
