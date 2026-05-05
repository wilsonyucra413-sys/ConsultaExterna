using ConsultaExterna.DTO;
using ConsultaExterna.Mapeadores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ConsultaExterna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestionCitasTurnosController : ControllerBase
    {
        public readonly HttpClient httpClient;
        public readonly string URL = "http://10.77.200.47:8000/";

        public GestionCitasTurnosController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        [HttpGet("PacientesEnEspera")]
        public async Task<IActionResult> GetPacientesEnEspera(string? fecha)
        {
            var url = string.IsNullOrEmpty(fecha)
                ? $"{URL}api/turnos/pacientes_en_espera/"
                : $"{URL}api/turnos/pacientes_en_espera/?fecha={fecha}";

            var data = await httpClient.GetFromJsonAsync<GestionCitasTurnosDTO>(url);

            return Ok(data);
        }
        [HttpGet("VerificarCita/{codigo}")]
        public async Task<IActionResult> GetVerificarCita(string codigo)
        {
            var url = $"{URL}api/turnos/verificar_cita/?codigo={codigo}";

            var data = await httpClient.GetFromJsonAsync<GestionCitasTurnosDTO>(url);

            return Ok(data);
        }
        [HttpGet("AgendaDelDia")]
        public async Task<IActionResult> GetAgendaDelDia(string fecha)
        {
            var url = $"{URL}api/turnos/agenda_del_dia/?fecha={fecha}";

            var data = await httpClient.GetFromJsonAsync<GestionCitasTurnosDTO>(url);

            return Ok(data);
        }


    }
}
