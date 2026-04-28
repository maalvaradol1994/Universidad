using Universidad.Dominio.Entidades;

namespace Universidad.Dominio.Repositorios
{
    public interface IProfesorRepositorio
    {
        Task AgregarProfesor(Profesor profesor);
    }
}
