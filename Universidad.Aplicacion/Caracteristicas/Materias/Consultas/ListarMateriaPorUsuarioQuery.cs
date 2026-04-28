using MediatR;
using Universidad.Aplicacion.Caracteristicas.Materias.DTOS;

namespace Universidad.Aplicacion.Caracteristicas.Materias.Consultas;

public record ListarMateriaPorUsuarioQuery(string usuario) : IRequest<List<MateriaDto>>;