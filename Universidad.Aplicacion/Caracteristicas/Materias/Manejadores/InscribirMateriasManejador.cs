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
            // 1. Obtener el estudiante
            Estudiante? estudiante = await estudianteRepositorio.ObtenerEstudiantePorId(request.EstudianteId);
            ValidarEstudianteExiste(estudiante);

            // 2. Obtener las materias reales desde la base de datos
            List<Materia> materiasSeleccionadas = await materiaRepositorio.ObtenerMateriaPorId(request.MateriasIds);
            ValidarMateriasSolicitadasExisten(materiasSeleccionadas, request.MateriasIds);

            // 3. El momento clave: Delegamos la lógica al Dominio. 
            estudiante!.InscribirMaterias(materiasSeleccionadas);

            // 4. Guardar los cambios
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

        private static void ValidarMateriasSolicitadasExisten(List<Materia> materiasEncontradas, List<int> idsSolicitados)
        {
            if (materiasEncontradas.Count != idsSolicitados.Count)
            {
                throw new ReglaNegocioExcepcion("Una o más materias seleccionadas no existen o no están disponibles.");
            }
        }
    }
}
