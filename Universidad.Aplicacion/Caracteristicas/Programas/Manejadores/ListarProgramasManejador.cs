using MediatR;
using Universidad.Aplicacion.Caracteristicas.Programas.Consultas;
using Universidad.Aplicacion.Caracteristicas.Programas.DTOS;
using Universidad.Dominio.Entidades;
using Universidad.Dominio.Repositorios;

namespace Universidad.Aplicacion.Caracteristicas.Programas.Manejadores;

public class ListarProgramasManejador(IProgramaRepositorio programaRepositorio) : IRequestHandler<ListarProgramasQuery, List<ProgramaDto>>
{
    public async Task<List<ProgramaDto>> Handle(ListarProgramasQuery request, CancellationToken cancellationToken)
    {
        List<Programa> programas = await programaRepositorio.ObtenerTodos();
        return [.. programas.Select(programa => new ProgramaDto(programa.Id, programa.Nombre, programa.TotalCreditos))];
    }
}
