using Universidad.Dominio.Entidades;

namespace Universidad.Dominio.Repositorios
{
    public interface IMateriaRepositorio
    {
        Task<List<Materia>> ObtenerMateriasDisponiblesIncribir(List<Materia> materias, int estudianteId);
        Task<List<Materia>> ObtenerTodas();
        Task<List<Materia>> ObtenerMateriasEstudiantes(int estudianteId);
        Task<bool> GuardarMaterias(List<Dominio.Entidades.Materia> materias, int estudianteId);
    }
}
