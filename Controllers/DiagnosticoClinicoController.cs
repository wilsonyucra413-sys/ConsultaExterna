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
        //https://diagnosticoseguro2-3.onrender.com/api/Resultadoes/ConsultaMedico/O-1
        public DiagnosticoClinicoController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        [HttpGet("consulta-medico/{ordenCodigo}")]
        public async Task<IActionResult> GetConsultaMedico(string ordenCodigo)
        {
            // URL que pasaste: https://diagnosticoseguro2-3.onrender.com/api/Resultadoes/ConsultaMedico/O-1
            var urlFinal = $"{URL}api/Resultadoes/ConsultaMedico/{ordenCodigo}";

            try
            {
                var respuesta = await httpClient.GetFromJsonAsync<ResultadoResponseDTO>(urlFinal);

                if (respuesta == null || respuesta.Data == null)
                {
                    return NotFound("No se encontraron resultados.");
                }

                return Ok(respuesta.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
        //post ordenexamen
        [HttpPost("crear-orden-examen")]
        public async Task<IActionResult> CrearOrdenExamen(OrdenExamenDTO orden)
        {
            //api/OrdenExamen?OrdenCodigo=daf&ExamenCodigo=dsaf&MuestraCodigo=sdf&AreaLaboratorio=adsfsd
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
