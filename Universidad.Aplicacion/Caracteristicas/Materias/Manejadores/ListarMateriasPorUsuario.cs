using MediatR;
using Universidad.Aplicacion.Caracteristicas.Materias.Consultas;
using Universidad.Aplicacion.Caracteristicas.Materias.DTOS;
using Universidad.Dominio.Entidades;
using Universidad.Dominio.Excepciones;
using Universidad.Dominio.Repositorios;

namespace Universidad.Aplicacion.Caracteristicas.Materias.Manejadores;

public class ListarMateriasPorUsuario(IMateriaRepositorio materiaRepositorio, IEstudianteRepositorio estudianteRepositorio): IRequestHandler<ListarMateriaPorUsuarioQuery, List<MateriaDto>>
{
    public async Task<List<MateriaDto>> Handle(ListarMateriaPorUsuarioQuery request, CancellationToken cancellationToken)
    {
        Estudiante? estudiante = await estudianteRepositorio.ObtenerEstudiantePorNombreUsuario(request.usuario);
        ValidarUsuario(estudiante);

        List<Materia> materias = await materiaRepositorio.ObtenerMateriasEstudiantes(estudiante.Id);
        return [.. materias.Select(m => new MateriaDto(m.Id, m.Nombre, m.Creditos, m.ProfesorId, m.ProfesorNombre))];
    }

    private static void ValidarUsuario(Estudiante? estudiante)
    {
        if (estudiante == null) {
            throw new ReglaNegocioExcepcion("El estudiante no existe en el sistema.");
        }
    }
}

