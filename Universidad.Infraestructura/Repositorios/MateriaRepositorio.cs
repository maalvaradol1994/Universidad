using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Universidad.Dominio.Repositorios;
using Universidad.Infraestructura.Persistencia;

namespace Universidad.Infraestructura.Repositorios
{
    public class MateriaRepositorio(UniversidadContexto contexto) : IMateriaRepositorio
    {
        public async Task<List<Universidad.Dominio.Entidades.Materia>> ObtenerMateriasDisponiblesIncribir(List<Universidad.Dominio.Entidades.Materia> materias, int estudianteId)
        {
            if (materias == null || !materias.Any()) return new List<Universidad.Dominio.Entidades.Materia>();

            var materiasUnicas = materias.DistinctBy(m => m.Id).ToList();

            var materiasInscritasIds = await contexto.Estudiante_Materia_Profesors
                .AsNoTracking() 
                .Where(emp => emp.estmatpr_estudiante_id == estudianteId && emp.estmatpr_profesor_materia != null && emp.estmatpr_profesor_materia.profmat_materia_id != null)
                .Select(emp => (int)emp.estmatpr_profesor_materia!.profmat_materia_id!)
                .ToListAsync();

            return materiasUnicas
                .Where(m => !materiasInscritasIds.Contains(m.Id))
                .ToList();
        }

        public async Task<List<Universidad.Dominio.Entidades.Materia>> ObtenerTodas()
        {
            return await contexto.Materias
                .AsNoTracking()
                .Select(detallesMateria)
                .ToListAsync();
        }

        private static readonly Expression<Func<Persistencia.Materia, Universidad.Dominio.Entidades.Materia>> detallesMateria =
            m => new Universidad.Dominio.Entidades.Materia
            (
                m.mat_id,
                m.mat_nombre ?? "",
                m.Profesor_Materia.Select(pm => pm.profmat_profesor_id).FirstOrDefault() ?? 0,
                m.Profesor_Materia.Select(pm => pm.profmat_profesor.prof_nombre).FirstOrDefault() ?? "",
                m.mat_valor_creditos ?? 3
            );
    }
}
