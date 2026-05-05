using ConsultaExterna.Dominio;
using ConsultaExterna.DTO;
using ConsultaExterna.Mapeadores;
using Microsoft.AspNetCore.Mvc;

namespace ConsultaExterna.Controllers
{
    public class DiagnosticoClinicoController : Controller
    {
        public readonly HttpClient httpClient;
        public readonly string URL = "http://10.10.5.225:5141/";
        public DiagnosticoClinicoController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        [HttpGet("mostrar")]
        public async Task<IActionResult> GetDiagnosticoClinicoDTO()
        {
            var examenes = await httpClient
                .GetFromJsonAsync<List<DiagnosticoClinicoDTO>>(URL + "api/Examen");
            var dto = examenes
                .Select(x => x.ToDiagnosticoDTO())
                .ToList();

            return Ok(dto);
        }
    }
}
