using MediatR;
using Universidad.Aplicacion.Caracteristicas.Estudiantes.DTOS;

namespace Universidad.Aplicacion.Caracteristicas.Estudiantes.Consultas;

public record ListarEstudiantesQuery() : IRequest<List<EstudianteDto>>;