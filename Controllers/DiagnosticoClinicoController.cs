using ConsultaExterna.Dominio;
using ConsultaExterna.DTO;
using ConsultaExterna.Mapeadores;
using Microsoft.AspNetCore.Mvc;

namespace ConsultaExterna.Controllers
{
    public class DiagnosticoClinicoController : Controller
    {
        public readonly HttpClient httpClient;
        public readonly string URL = "https://diagnosticoseguro2-3.onrender.com/";
        public DiagnosticoClinicoController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        [HttpGet("consulta-medico/{ordenCodigo}")]
        public async Task<IActionResult> GetConsultaMedico(string ordenCodigo)
        {
            var urlFinal = $"{URL}api/Resultadoes/ConsultaMedico/{ordenCodigo}";

            var resultados = await httpClient.GetFromJsonAsync<List<ResultadoDTO>>(urlFinal);

            if (resultados == null || !resultados.Any())
            {
                return NotFound($"No se encontraron resultados para la orden: {ordenCodigo}");
            }

            return Ok(resultados);

        }
        //post ordenexamen
        [HttpPost("crear-orden-examen")]
        public async Task<IActionResult> CrearOrdenExamen(OrdenExamenDTO orden)
        {
            var url = $"{URL}api/OrdenExamen" +
                      $"?OrdenCodigo={orden.OrdenCodigo}" +
                      $"&ExamenCodigo={orden.ExamenCodigo}" +
                      $"&MuestraCodigo={orden.MuestraCodigo}" +
                      $"&AreaLaboratorio={orden.AreaLaboratorio}";

            var response = await httpClient.PostAsync(url, null);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Error al consumir el API");
            }

            var result = await response.Content.ReadAsStringAsync();
            return Ok(result);
        }
        [HttpPost("hacer-orden-laboratorio")]
        public async Task<IActionResult> HacerOrdenLaboratorio([FromBody] OrdenLaboratorioDTO orden)
        {
            var url = $"{URL}api/OrdenLaboratorio/HacerOrdenLaboratorio" +
                      $"?code={orden.Code}" +
                      $"&PacienteCodigo={orden.PacienteCodigo}" +
                      $"&MedicoCodigo={orden.MedicoCodigo}" +
                      $"&FechaOrden={orden.FechaOrden.ToString("yyyy-MM-dd")}" +
                      $"&TipoAtencion={orden.TipoAtencion}" +
                      $"&Observaciones={orden.Observaciones}";

            var response = await httpClient.PostAsync(url, null);

            if (!response.IsSuccessStatusCode)
            {
                var errorMsg = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, $"Error en la API destino: {errorMsg}");
            }

            var result = await response.Content.ReadAsStringAsync();
            return Ok(new { mensaje = "Orden creada con éxito", respuesta = result });
        }
    }
}
