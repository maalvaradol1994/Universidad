using Universidad.Dominio.Entidades;

namespace Universidad.Dominio.Repositorios
{
    public interface IProgramaRepositorio
    {
        Task<List<Programa>> ObtenerTodos();
        Task<Programa?> ObtenerPorId(int id);
        Task<bool> ExisteInscripcion(int estudianteId, int programaId);
        Task AsignarEstudianteAPrograma(int estudianteId, int programaId);
    }
}
