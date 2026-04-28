using MediatR;
using Universidad.Aplicacion.Caracteristicas.Materias.Comandos;
using Universidad.Dominio.Entidades;
using Universidad.Dominio.Excepciones;
using Universidad.Dominio.Repositorios;

namespace Universidad.Aplicacion.Caracteristicas.Materias.Manejadores
{
    public class InscribirMateriasManejador(IEstudianteRepositorio estudianteRepositorio, IMateriaRepositorio materiaRepositorio) : IRequestHandler<InscribirMateriasComando, bool>
    {
        public async Task<bool> Handle(InscribirMateriasComando request, CancellationToken cancellationToken)
        {

            Estudiante? estudiante = await estudianteRepositorio.ObtenerEstudiantePorNombreUsuario(request.Usuario);
            ValidarEstudianteExiste(estudiante);

            List<Materia> materiasDisponibles = await materiaRepositorio.ObtenerMateriasDisponiblesIncribir(request.Materias, estudiante!.Id);
            ValidarMateriasSolicitadasExisten(materiasDisponibles, request.Materias);

            ValidarMateriasSoloUnProfesor(request.Materias);

            estudiante!.InscribirMaterias(request.Materias);

            await estudianteRepositorio.ActualizarEstudiante(estudiante);

            return true;
        }

        private static void ValidarEstudianteExiste(Estudiante? estudiante)
        {
            if (estudiante == null)
            {
                throw new ReglaNegocioExcepcion("El estudiante no existe en el sistema.");
            }
        }

        private static void ValidarMateriasSolicitadasExisten(List<Materia> materiasEncontradas, List<Materia> idsSolicitados)
        {
            if (materiasEncontradas.Count != idsSolicitados.Count)
            {
                throw new ReglaNegocioExcepcion("Una o más materias seleccionadas no existen o no están disponibles.");
            }
        }

        private static void ValidarMateriasSoloUnProfesor(List<Materia> materias)
        {
            if (materias.Select(x => x.ProfesorId).Count() > 1)
            {
                throw new ReglaNegocioExcepcion("No se pueden inscribir materias con el mismo profesor.");
            }
        }
    }
}
