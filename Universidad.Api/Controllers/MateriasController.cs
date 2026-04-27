using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Universidad.Api.Compartido.Respuestas;
using Universidad.Aplicacion.Caracteristicas.Estudiantes.Consultas;
using Universidad.Aplicacion.Caracteristicas.Estudiantes.DTOS;
using Universidad.Aplicacion.Caracteristicas.Materias.Comandos;
using Universidad.Aplicacion.Caracteristicas.Materias.Consultas;
using Universidad.Aplicacion.Caracteristicas.Materias.DTOS;

namespace Universidad.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MateriasController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Estudiante")]
    public async Task<IActionResult> ObtenerMaterias()
    {
        ListarMateriasQuery query = new();
        List<MateriaDto> materias = await mediator.Send(query);

        RespuestaGeneral<List<MateriaDto>> respuesta = new RespuestaGeneral<List<MateriaDto>>
        {
            Exitoso = true,
            Mensaje = "Catálogo de materias obtenido",
            Resultado = materias,
            StatusCode = StatusCodes.Status200OK
        };

        return Ok(respuesta);
    }

    [HttpGet("{id}/estudiantes")]
    [Authorize(Roles = "Estudiante")]
    public async Task<IActionResult> ObtenerCompaneros(int id)
    {
        ObtenerCompanerosQuery query = new(id);
        List<EstudianteDto> companeros = await mediator.Send(query);

        RespuestaGeneral<List<EstudianteDto>> respuesta = new()
        {
            Exitoso = true,
            Mensaje = "Lista de compañeros obtenida",
            Resultado = companeros,
            StatusCode = StatusCodes.Status200OK
        };

        return Ok(respuesta);
    }

}