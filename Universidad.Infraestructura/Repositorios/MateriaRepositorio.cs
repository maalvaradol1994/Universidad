using Microsoft.EntityFrameworkCore;
using Universidad.Dominio.Entidades;
using Universidad.Dominio.Repositorios;
using Universidad.Infraestructura.Persistencia;

namespace Universidad.Infraestructura.Repositorios
{
    public class MateriaRepositorio(UniversidadContexto contexto) : IMateriaRepositorio
    {
        public async Task<List<Universidad.Dominio.Entidades.Materia>> ObtenerMateriaPorId(IEnumerable<int> ids)
        {
            return await contexto.Profesor_Materia
                .Where(materia => ids.Contains(materia.profmat_id))
                .Select(m => new Universidad.Dominio.Entidades.Materia
                (
                    m.profmat_materia.mat_nombre, 
                    m.profmat_profesor.prof_id
                ))
                .ToListAsync();
        }

        public async Task<List<Universidad.Dominio.Entidades.Materia>> ObtenerTodas()
        {
            return await contexto.Profesor_Materia
                .Select(m => new Universidad.Dominio.Entidades.Materia
                (
                    m.profmat_materia.mat_nombre,
                    m.profmat_profesor.prof_id
                ))
                .ToListAsync();
        }
    }
}
