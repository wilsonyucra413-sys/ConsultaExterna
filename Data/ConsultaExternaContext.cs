using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConsultaExterna.Dominio;

namespace ConsultaExterna.Data
{
    public class ConsultaExternaContext : DbContext
    {
        public ConsultaExternaContext (DbContextOptions<ConsultaExternaContext> options)
            : base(options)
        {
        }

        public DbSet<Consulta> Consulta { get; set; } = default!;
        public DbSet<TipoDiagnostico> TipoDiagnostico { get; set; } = default!;
        public DbSet<DiagnosticoConsulta> DiagnosticoConsulta { get; set; } = default!;
        public DbSet<NotaEvaluacion> NotaEvaluacion { get; set; } = default!;
    }
}
