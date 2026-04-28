using Universidad.Dominio.Entidades;

namespace Universidad.Dominio.Repositorios
{
    public interface IMateriaRepositorio
    {
        Task<List<Materia>> ObtenerMateriasDisponiblesIncribir(List<Materia> materias, int estudianteId);
        Task<List<Materia>> ObtenerTodas();
    }
}
