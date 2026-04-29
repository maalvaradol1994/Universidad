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

            if (estudiante == null)
            {
                return null;
            }

            return new Dominio.Entidades.Estudiante(
                estudiante.est_id,
                estudiante.est_nombre,
                string.Empty,
                string.Empty,
                estudiante.est_identificacion,
                estudiante.est_usuario_id ?? 0);
        }

        public async Task<Dominio.Entidades.Estudiante?> ObtenerEstudiantePorNombreUsuario(string nombreUsuario)
        {
            return await contexto.Estudiantes
                .Where(estudiante => estudiante.est_usuario != null && estudiante.est_usuario.usu_usuario == nombreUsuario)
                .Select(estudiante => new Dominio.Entidades.Estudiante(
                    estudiante.est_id,
                    estudiante.est_nombre,
                    estudiante.est_usuario!.usu_usuario,
                    string.Empty,
                    estudiante.est_identificacion,
                    estudiante.est_usuario_id ?? 0))
                .FirstOrDefaultAsync();
        }

        public async Task AgregarEstudiante(Universidad.Dominio.Entidades.Estudiante estudiante)
        {
            Universidad.Infraestructura.Persistencia.Estudiante nuevoEstudiante = new()
            {
                est_id = estudiante.Id,
                est_nombre = estudiante.Nombre,
                est_activo = 1,
                est_identificacion = estudiante.Identificacion,
                est_usuario_id = estudiante.UsuarioId,
            };
            await contexto.Estudiantes.AddAsync(nuevoEstudiante);
            await contexto.SaveChangesAsync();
        }

        public async Task ActualizarEstudiante(Dominio.Entidades.Estudiante estudiante)
        {
            Persistencia.Estudiante actualizarEstudiante = new()
            {
                est_id = estudiante.Id,
                est_nombre = estudiante.Nombre,
                est_activo = 1,
                est_identificacion = estudiante.Identificacion
            };
            contexto.Estudiantes.Update(actualizarEstudiante);
            await contexto.SaveChangesAsync();
        }

        public async Task<List<Dominio.Entidades.Estudiante>> ObtenerTodos()
        {
            return await contexto.Estudiantes
                .Select(e => new Dominio.Entidades.Estudiante(
                    e.est_id,
                    e.est_nombre,
                    string.Empty,
                    string.Empty,
                    e.est_identificacion,
                    e.est_usuario_id ?? 0))
                .ToListAsync();
        }

        public async Task<List<Universidad.Dominio.Entidades.Estudiante>> ObtenerEstudiantesPorMateria(int materiaId)
        {
            return await contexto.Estudiantes_Por_Materias
                .Where(e => e.id_materia == materiaId)
                .Select(m => new Universidad.Dominio.Entidades.Estudiante(
                    m.estudiante_nombre, 
                    string.Empty,
                    string.Empty,
                    m.identificacion!, 
                    0))
                .ToListAsync();
        }
    }
}
