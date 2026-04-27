using MediatR;
using Universidad.Aplicacion.Caracteristicas.Programas.Comandos;
using Universidad.Dominio.Entidades;
using Universidad.Dominio.Excepciones;
using Universidad.Dominio.Repositorios;

namespace Universidad.Aplicacion.Caracteristicas.Programas.Manejadores;

public class AsignarProgramaEstudianteManejador(
    IEstudianteRepositorio estudianteRepositorio,
    IProgramaRepositorio programaRepositorio) : IRequestHandler<AsignarProgramaEstudianteComando, bool>
{
    public async Task<bool> Handle(AsignarProgramaEstudianteComando request, CancellationToken cancellationToken)
    {
        Estudiante? estudiante = await estudianteRepositorio.ObtenerEstudiantePorNombreUsuario(request.NombreUsuario);
        ValidarEstudianteExiste(estudiante);

        Programa? programa = await programaRepositorio.ObtenerPorId(request.ProgramaId);
        ValidarProgramaExiste(programa);

        bool yaInscrito = await programaRepositorio.ExisteInscripcion(estudiante!.Id, request.ProgramaId);
        if (yaInscrito)
        {
            throw new ReglaNegocioExcepcion("El estudiante ya se encuentra inscrito en este programa.");
        }

        await programaRepositorio.AsignarEstudianteAPrograma(estudiante.Id, request.ProgramaId);

        return true;
    }

    private static void ValidarEstudianteExiste(Estudiante? estudiante)
    {
        if (estudiante == null)
        {
            throw new ReglaNegocioExcepcion("No se encontró un estudiante asociado al usuario autenticado.");
        }
    }

    private static void ValidarProgramaExiste(Programa? programa)
    {
        if (programa == null)
        {
            throw new ReglaNegocioExcepcion("El programa seleccionado no existe.");
        }
    }
}
