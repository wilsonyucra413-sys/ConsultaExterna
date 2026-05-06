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
        [HttpGet("MostrarOrdenLaboratorio")]
        public async Task<IActionResult> ProbarConexion()
        {
            // URL directa para listar todo
            string urlPrueba = "https://diagnosticoseguro2-3.onrender.com/api/Resultadoes";

            try
            {
                // Consumimos el objeto que contiene la lista bajo la clave "resultados"
                var respuesta = await httpClient.GetFromJsonAsync<ResultadoListResponseDTO>(urlPrueba);

                if (respuesta == null || respuesta.Resultados == null)
                {
                    return NotFound("Conectó, pero la lista de resultados vino vacía.");
                }

                // Si esto funciona, devuelve la lista y confirma la conexión
                return Ok(new
                {
                    estado = "Conexión Exitosa",
                    mensajeServicioDestino = respuesta.Mensaje,
                    datos = respuesta.Resultados
                });
            }
            catch (Exception ex)
            {
                // Si entra aquí, nos dirá el error técnico real
                return StatusCode(500, new
                {
                    estado = "Error de Conexión",
                    error = ex.Message,
                    urlIntentada = urlPrueba
                });
            }
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
        public async Task<IActionResult> HacerOrdenLaboratorio( OrdenLaboratorioDTO orden)
        {
            string fechaFormateada = orden.FechaOrden.ToString("yyyy-MM-dd");

            var urlFinal = $"{URL}api/OrdenLaboratorio/HacerOrdenLaboratorio" +
                          $"?code={Uri.EscapeDataString(orden.Code)}" +
                          $"&PacienteCodigo={Uri.EscapeDataString(orden.PacienteCodigo)}" +
                          $"&MedicoCodigo={Uri.EscapeDataString(orden.MedicoCodigo)}" +
                          $"&FechaOrden={fechaFormateada}" +
                          $"&TipoAtencion={Uri.EscapeDataString(orden.TipoAtencion)}" +
                          $"&Observaciones={Uri.EscapeDataString(orden.Observaciones)}";

            try
            {
                var response = await httpClient.PostAsync(urlFinal, null);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, $"Error en Laboratorio: {errorMsg}");
                }

                var result = await response.Content.ReadAsStringAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error de comunicación: {ex.Message}");
            }
        }
    }

}
