using MediatR;
using Universidad.Aplicacion.Caracteristicas.Estudiantes.DTOS;

namespace Universidad.Aplicacion.Caracteristicas.Estudiantes.Consultas;

public record ObtenerCompanerosQuery(int MateriaId) : IRequest<List<EstudianteDto>>;

