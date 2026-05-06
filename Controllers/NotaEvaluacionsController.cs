using ConsultaExterna.Data;
using ConsultaExterna.Dominio;
using ConsultaExterna.DTO;
using ConsultaExterna.Mapeadores;
using Humanizer;
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
    public class NotaEvaluacionsController : ControllerBase
    {
        private readonly ConsultaExternaContext context;

        public NotaEvaluacionsController(ConsultaExternaContext context)
        {
            this.context = context;
        }

        // GET: api/NotaEvaluacions
        [HttpGet("MostrarNotaEvaluacion")]
        public async Task<IActionResult> GetNotaEvaluacion()
        {
            var pe = await context.NotaEvaluacion
                .Include(n => n.Consulta)
                .Where(n => n.Estado == "Activo")
                .Select(n => n.ToNotaEvaluacionDTO())
                .ToListAsync();
            return Ok(pe);
        }

        // GET: api/NotaEvaluacions/5
        [HttpGet("BuscarNotaEvaluacion/{codigo}")]
        public async Task<IActionResult> GetNotaEvaluacion(string codigo)
        {
            var pe = await context.NotaEvaluacion
                .Include(n => n.Consulta)
                .FirstOrDefaultAsync(n => n.Codigo == codigo);

            if (pe == null)
                return NotFound(new { mensaje = "No existe la nota" });
            if(pe.Estado == "Inactivo")
                return NotFound(new { mensaje = "La nota esta inactiva" });
            return Ok(pe.ToNotaEvaluacionDTO());
        }

        // PUT: api/NotaEvaluacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("ActualizarNotaEvaluacion/{codigo}")]
        public async Task<IActionResult> PutNotaEvaluacion(string codigo, UpdateNotaEvaluacionDTO nota)
        {
            var pe = await context.NotaEvaluacion
            .Include(n => n.Consulta)
            .FirstOrDefaultAsync(n => n.Codigo == codigo);

            if (pe == null)
                return NotFound(new { mensaje = "No existe la nota" });

            if (pe.Estado == "Inactivo")
                return BadRequest(new { mensaje = "La nota está inactiva" });

            pe.Fecha = nota.Fecha;
            pe.Descripcion = nota.Descripcion;

            await context.SaveChangesAsync();

            return Ok(new
            {
                mensaje = "Nota actualizada",
                nota = pe.ToNotaEvaluacionDTO()
            });
        }

        // POST: api/NotaEvaluacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("CrearNotaEvaluacion/{codigo}")]
        public async Task<IActionResult> PostNotaEvaluacion(string codigo, CreateNotaEvaluacionDTO nota)
        {
            var pe = await context.NotaEvaluacion
                 .FirstOrDefaultAsync(n => n.Codigo == codigo);

            if (pe != null && pe.Estado == "Activo")
                return BadRequest(new { mensaje = "La nota ya existe" });

            if (pe != null && pe.Estado == "Inactivo")
                return BadRequest(new { mensaje = "La nota está inactiva" });

            var con = await context.Consulta
                .FirstOrDefaultAsync(c => c.Codigo == nota.CodigoConsulta);

            if (con == null)
                return BadRequest(new { mensaje = "No existe la consulta" });

            var nv = new NotaEvaluacion
            {
                Codigo = codigo,
                Fecha = nota.Fecha,
                Descripcion = nota.Descripcion,
                IdConsulta = con.IdConsulta,
                Estado = "Activo"
            };

            await context.NotaEvaluacion.AddAsync(nv);
            await context.SaveChangesAsync();

            var res = await context.NotaEvaluacion
                .Include(n => n.Consulta)
                .FirstOrDefaultAsync(n => n.Codigo == codigo);

            if (res == null)
                return BadRequest(new { mensaje = "Error al crear la nota" });

            return Ok(new
            {
                mensaje = "Nota creada correctamente",
                nota = res.ToNotaEvaluacionDTO()
            });
        }

        // DELETE: api/NotaEvaluacions/5
        [HttpDelete("EliminarNotaEvaluacion/{codigo}")]
        public async Task<IActionResult> DeleteNotaEvaluacion(string codigo)
        {
            var pe = await context.NotaEvaluacion
                .Include(n => n.Consulta)
                .FirstOrDefaultAsync(e => e.Codigo == codigo);
            if (pe == null)
            {
                return NotFound(new { mensaje = "No existe ese codigo" });
            }
            if (pe.Estado == "Inactivo")
            {
                return NotFound(new { mensaje = "Ese codigo ya esta inactivo" });
            }

            pe.Estado = "Inactivo";
            await context.SaveChangesAsync();
            return Ok(pe.ToNotaEvaluacionDTO());
        }

    }
}
