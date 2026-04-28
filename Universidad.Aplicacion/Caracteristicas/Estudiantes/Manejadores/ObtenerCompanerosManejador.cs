using MediatR;
using Universidad.Aplicacion.Caracteristicas.Estudiantes.Consultas;
using Universidad.Aplicacion.Caracteristicas.Estudiantes.DTOS;
using Universidad.Dominio.Entidades;
using Universidad.Dominio.Repositorios;

namespace Universidad.Aplicacion.Caracteristicas.Estudiantes.Manejadores;

public class ObtenerCompanerosManejador(IEstudianteRepositorio estudianteRepositorio): IRequestHandler<ObtenerCompanerosQuery, List<EstudianteDto>>
{
    public async Task<List<EstudianteDto>> Handle(ObtenerCompanerosQuery request, CancellationToken cancellationToken)
    {
        List<Estudiante> estudiantes = await estudianteRepositorio.ObtenerEstudiantesPorMateria(request.MateriaId);
        return [.. estudiantes.Select(e => new EstudianteDto(
            e.Id, e.Nombre, e.Correo, e.CreditosDisponibles, []
        ))];
    }
}

