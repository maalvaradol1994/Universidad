using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Universidad.Api.Compartido.Respuestas;
using Universidad.Aplicacion.Caracteristicas.Programas.Comandos;
using Universidad.Aplicacion.Caracteristicas.Programas.Consultas;
using Universidad.Aplicacion.Caracteristicas.Programas.DTOS;
using Universidad.Dominio.Excepciones;

namespace Universidad.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProgramasController(IMediator mediator) : ControllerBase
{
    public record AsignarProgramaRequest(int ProgramaId);

    [HttpGet]
    [Authorize(Roles = "Estudiante")]
    public async Task<IActionResult> ObtenerProgramas()
    {
        ListarProgramasQuery query = new();
        List<ProgramaDto> programas = await mediator.Send(query);

        RespuestaGeneral<List<ProgramaDto>> respuesta = new()
        {
            Exitoso = true,
            Mensaje = "Catálogo de programas obtenido",
            Resultado = programas,
            StatusCode = StatusCodes.Status200OK
        };

        return Ok(respuesta);
    }

    [HttpPost]
    [Authorize(Roles = "Estudiante")]
    public async Task<IActionResult> AsignarPrograma([FromBody] AsignarProgramaRequest request)
    {
        string? nombreUsuario = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        if (string.IsNullOrWhiteSpace(nombreUsuario))
        {
            throw new ReglaNegocioExcepcion("No fue posible identificar el usuario autenticado.");
        }

        AsignarProgramaEstudianteComando comando = new(nombreUsuario, request.ProgramaId);
        await mediator.Send(comando);

        RespuestaGeneral<string> respuesta = new()
        {
            Exitoso = true,
            Mensaje = "Programa asignado con éxito",
            Resultado = null,
            StatusCode = StatusCodes.Status200OK
        };

        return Ok(respuesta);
    }
}
