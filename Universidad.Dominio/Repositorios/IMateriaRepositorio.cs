using Universidad.Dominio.Entidades;

namespace Universidad.Dominio.Repositorios
{
    public interface IMateriaRepositorio
    {
        Task<List<Materia>> ObtenerMateriaPorId(IEnumerable<int> id);
        Task<List<Materia>> ObtenerTodas();
    }
}
