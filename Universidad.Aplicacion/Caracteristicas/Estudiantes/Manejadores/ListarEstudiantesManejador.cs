using MediatR;
using Universidad.Dominio.Entidades;
using Universidad.Dominio.Repositorios;
using Universidad.Aplicacion.Caracteristicas.Estudiantes.Consultas;
using Universidad.Aplicacion.Caracteristicas.Estudiantes.DTOS;

namespace Universidad.Aplicacion.Caracteristicas.Estudiantes.Manejadores;

public class ListarEstudiantesManejador(IEstudianteRepositorio estudianteRepositorio): IRequestHandler<ListarEstudiantesQuery, List<EstudianteDto>>
{
    public async Task<List<EstudianteDto>> Handle(ListarEstudiantesQuery request, CancellationToken cancellationToken)
    {
        // 1. Va a la base de datos
        List<Estudiante> estudiantes = await estudianteRepositorio.ObtenerTodos();

        // 2. Transforma las entidades en DTOs para que sean seguros de enviar
        return [.. estudiantes.Select(e => new EstudianteDto(
            e.Id, e.Nombre, e.Correo, e.CreditosDisponibles, []
        ))];
    }
}