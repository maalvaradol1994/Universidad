using Universidad.Dominio.Entidades;

namespace Universidad.Dominio.Repositorios
{
    public interface IEstudianteRepositorio
    {
        Task<Estudiante?> ObtenerEstudiantePorId(int id);
        Task AgregarEstudiante(Estudiante estudiante);
        Task ActualizarEstudiante(Estudiante estudiante);
        Task<List<Estudiante>> ObtenerTodos();
        Task<List<Estudiante>> ObtenerEstudiantesPorMateria(int materiaId);
    }
}
