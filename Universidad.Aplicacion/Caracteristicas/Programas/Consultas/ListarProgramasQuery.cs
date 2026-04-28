using MediatR;
using Universidad.Aplicacion.Caracteristicas.Programas.DTOS;

namespace Universidad.Aplicacion.Caracteristicas.Programas.Consultas;

public record ListarProgramasQuery() : IRequest<List<ProgramaDto>>;
