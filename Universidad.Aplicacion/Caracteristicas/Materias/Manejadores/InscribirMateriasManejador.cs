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

            InscribirMaterias(request.Materias, estudiante.Id);

            await estudianteRepositorio.ActualizarEstudiante(estudiante);

            return true;
        }

        public void InscribirMaterias(List<Materia> materiasAInscribir, int estudianteId)
        {
            ValidarCantidadMaterias(materiasAInscribir);
            ValidarProfesoresDiferentes(materiasAInscribir);
            ValidarCreditosSuficientes(materiasAInscribir);
            materiaRepositorio.GuardarMaterias(materiasAInscribir, estudianteId);
        }

        private static void ValidarCantidadMaterias(List<Materia> materias)
        {
            const int CantidadMaximaMaterias = 3;

            if (materias.Count > CantidadMaximaMaterias)
            {
                throw new ReglaNegocioExcepcion($"El estudiante debe seleccionar máximo {CantidadMaximaMaterias} materias.");
            }

            if (materias.Count == 0)
            {
                throw new ReglaNegocioExcepcion($"El estudiante debe seleccionar al menos 1 materia.");
            }
        }

        private static void ValidarProfesoresDiferentes(List<Materia> materias)
        {
            IEnumerable<int> profesoresIds = materias.Select(materia => materia.ProfesorId).Distinct();

            if (profesoresIds.Count() != materias.Count)
            {
                throw new ReglaNegocioExcepcion("No puedes seleccionar materias dictadas por el mismo profesor.");
            }
        }

        private static void ValidarCreditosSuficientes(List<Materia> materias)
        {
            int creditosRequeridos = materias.Sum(materia => materia.Creditos);

            if (creditosRequeridos > 9)
            {
                throw new ReglaNegocioExcepcion($"Créditos insuficientes. Necesitas {creditosRequeridos} créditos, pero solo tienes {9} disponibles.");
            }
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
            // Agrupamos por Id de profesor y vemos si algún grupo tiene más de 1 elemento
            bool tieneRepetidos = materias.GroupBy(x => x.ProfesorId)
                                          .Any(grupo => grupo.Count() > 1);

            if (tieneRepetidos)
            {
                throw new ReglaNegocioExcepcion("No se pueden inscribir materias con el mismo profesor.");
            }
        }
    }
}
