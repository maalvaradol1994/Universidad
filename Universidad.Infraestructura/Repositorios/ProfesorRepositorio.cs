using Universidad.Dominio.Entidades;
using Universidad.Dominio.Repositorios;
using Universidad.Infraestructura.Persistencia;

namespace Universidad.Infraestructura.Repositorios
{
    public class ProfesorRepositorio(UniversidadContexto contexto) : IProfesorRepositorio
    {
        public async Task AgregarProfesor(Profesor profesor)
        {
            Profesore nuevoProfesor = new()
            {
                prof_id = profesor.Id,
                prof_nombre = profesor.Nombre,
                prof_identificacion = profesor.Identificacion,
                prof_usuario_id = profesor.UsuarioId,
                prof_activo = 1
            };

            await contexto.Profesores.AddAsync(nuevoProfesor);
            await contexto.SaveChangesAsync();
        }
    }
}
