using MediatR;
using Universidad.Aplicacion.Caracteristicas.Estudiantes.DTOS;

namespace Universidad.Aplicacion.Caracteristicas.Estudiantes.Consultas;

public record ObtenerEstudiantePorIdQuery(int Id) : IRequest<EstudianteDto>;