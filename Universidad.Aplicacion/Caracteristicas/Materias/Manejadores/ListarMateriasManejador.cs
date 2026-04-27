using MediatR;
using Universidad.Aplicacion.Caracteristicas.Materias.Consultas;
using Universidad.Aplicacion.Caracteristicas.Materias.DTOS;
using Universidad.Dominio.Entidades;
using Universidad.Dominio.Repositorios;

namespace Universidad.Aplicacion.Caracteristicas.Materias.Manejadores;

public class ListarMateriasManejador(IMateriaRepositorio materiaRepositorio): IRequestHandler<ListarMateriasQuery, List<MateriaDto>>
{
    public async Task<List<MateriaDto>> Handle(ListarMateriasQuery request, CancellationToken cancellationToken)
    {
        List<Materia> materias = await materiaRepositorio.ObtenerTodas();
        return [.. materias.Select(m => new MateriaDto(m.Id, m.Nombre, m.Creditos))];
    }
}

