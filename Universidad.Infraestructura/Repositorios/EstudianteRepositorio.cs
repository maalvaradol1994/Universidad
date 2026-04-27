using Microsoft.EntityFrameworkCore;
using Universidad.Dominio.Entidades;
using Universidad.Dominio.Repositorios;
using Universidad.Infraestructura.Persistencia;

namespace Universidad.Infraestructura.Repositorios
{
    public class EstudianteRepositorio(UniversidadContexto contexto) : IEstudianteRepositorio
    {
        public async Task<Dominio.Entidades.Estudiante?> ObtenerEstudiantePorId(int id)
        {
            Universidad.Infraestructura.Persistencia.Estudiante? estudiante = await contexto.Estudiantes.FirstOrDefaultAsync(e => e.est_id == id);
            return new Dominio.Entidades.Estudiante(estudiante?.est_nombre!, string.Empty, string.Empty, estudiante?.est_identificacion!);
        }

        public async Task AgregarEstudiante(Universidad.Dominio.Entidades.Estudiante estudiante)
        {
            Universidad.Infraestructura.Persistencia.Estudiante nuevoEstudiante = new()
            {
                est_id = estudiante.Id,
                est_nombre = estudiante.Nombre,
                est_activo = 1,
                est_identificacion = estudiante.Identificacion
            };
            await contexto.Estudiantes.AddAsync(nuevoEstudiante);
            await contexto.SaveChangesAsync();
        }

        public async Task ActualizarEstudiante(Universidad.Dominio.Entidades.Estudiante estudiante)
        {
            Universidad.Infraestructura.Persistencia.Estudiante actualizarEstudiante = new()
            {
                est_id = estudiante.Id,
                est_nombre = estudiante.Nombre,
                est_activo = 1,
                est_identificacion = estudiante.Identificacion
            };
            contexto.Estudiantes.Update(actualizarEstudiante);
            await contexto.SaveChangesAsync();
        }

        public async Task<List<Universidad.Dominio.Entidades.Estudiante>> ObtenerTodos()
        {
            return await contexto.Estudiantes.Select(e => new Universidad.Dominio.Entidades.Estudiante(e.est_nombre, string.Empty, string.Empty, e.est_identificacion!)).ToListAsync();
        }

        public async Task<List<Universidad.Dominio.Entidades.Estudiante>> ObtenerEstudiantesPorMateria(int materiaId)
        {
            //return await contexto.Materias
            //    .Include(e => e.Profesor_Materia)
            //    .Where(e => e.mate.Any(m => m.Id == materiaId))
            //    .ToListAsync();
            throw new NotImplementedException();
        }
    }
}
