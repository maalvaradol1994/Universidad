using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Universidad.Api.Compartido.Respuestas;
using Universidad.Aplicacion.Caracteristicas.Estudiantes.Comandos;
using Universidad.Aplicacion.Caracteristicas.Estudiantes.Consultas;
using Universidad.Aplicacion.Caracteristicas.Estudiantes.DTOS;
using Universidad.Aplicacion.Caracteristicas.Materias.Comandos;

namespace Universidad.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EstudiantesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "Profesor")]
    public async Task<IActionResult> Registrar(RegistrarEstudianteComando comando)
    {
        int estudianteId = await mediator.Send(comando);

        RespuestaGeneral<int> respuesta = new()
        {
            Exitoso = true,
            Mensaje = "Estudiante registrado con éxito",
            Resultado = estudianteId,
            StatusCode = StatusCodes.Status200OK
        };

        return Ok(respuesta);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Profesor,Estudiante")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        ObtenerEstudiantePorIdQuery query = new(id);

        EstudianteDto estudiante = await mediator.Send(query);

        RespuestaGeneral<EstudianteDto> respuesta = new()
        {
            Exitoso = true,
            Mensaje = "Estudiante consultado correctamente",
            Resultado = estudiante,
            StatusCode = StatusCodes.Status200OK
        };

        return Ok(respuesta);
    }

    [HttpGet]
    [Authorize(Roles = "Profesor, Estudiante")]
    public async Task<IActionResult> ObtenerTodos([FromHeader] HttpRequestHeader header)
    {
        ListarEstudiantesQuery query = new();
        List<EstudianteDto> estudiantes = await mediator.Send(query);

        RespuestaGeneral<List<EstudianteDto>> respuesta = new()
        {
            Exitoso = true,
            Mensaje = "Lista de estudiantes obtenida",
            Resultado = estudiantes,
            StatusCode = StatusCodes.Status200OK
        };

        return Ok(respuesta);
    }

    [HttpPost("{id}/materias")]
    [Authorize(Roles = "Estudiante")]
    public async Task<IActionResult> InscribirMaterias(int id, [FromBody] List<int> materiasIds)
    {
        InscribirMateriasComando comando = new(id, materiasIds);

        await mediator.Send(comando);

        RespuestaGeneral<string> respuesta = new()
        {
            Exitoso = true,
            Mensaje = "Materias inscritas con éxito",
            Resultado = null,
            StatusCode = StatusCodes.Status200OK
        };

        return Ok(respuesta);
    }
}
