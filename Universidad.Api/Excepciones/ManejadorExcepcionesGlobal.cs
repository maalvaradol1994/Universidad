using Microsoft.AspNetCore.Diagnostics;
using Universidad.Api.Compartido.Respuestas;
using Universidad.Dominio.Excepciones;

namespace Universidad.Api.Excepciones
{
    public class ManejadorExcepcionesGlobal : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            RespuestaGeneral<object> respuestaError = new()
            {
                Exitoso = false,
                Resultado = null
            };

            if (exception is ReglaNegocioExcepcion reglaNegocioExcepcion)
            {
                respuestaError.StatusCode = StatusCodes.Status400BadRequest;
                respuestaError.Mensaje = reglaNegocioExcepcion.Message;
            }
            else
            {
                respuestaError.StatusCode = StatusCodes.Status500InternalServerError;
                respuestaError.Mensaje = "Ha ocurrido un error inesperado en el servidor. Por favor, contacte a soporte.";
            }

            httpContext.Response.StatusCode = respuestaError.StatusCode;

            await httpContext.Response.WriteAsJsonAsync(respuestaError, cancellationToken);

            return true;
        }
    }
}
