using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Universidad.Dominio.Repositorios;
using Universidad.Infraestructura.Persistencia;

namespace Universidad.Infraestructura.Repositorios
{
    public class MateriaRepositorio(UniversidadContexto contexto) : IMateriaRepositorio
    {
        public async Task<List<Dominio.Entidades.Materia>> ObtenerMateriasDisponiblesIncribir(List<Dominio.Entidades.Materia> materias, int estudianteId)
        {
            if (materias == null || !materias.Any()) return new List<Dominio.Entidades.Materia>();

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

        public async Task<List<Dominio.Entidades.Materia>> ObtenerTodas()
        {
            return await contexto.Materias
                .AsNoTracking()
                .Select(detallesMateria)
                .ToListAsync();
        }

        public async Task<List<Dominio.Entidades.Materia>> ObtenerMateriasEstudiantes(int estudianteId)
        {
            var consulta = await contexto.Estudiante_Materia_Profesors
                .AsNoTracking()
                .Where(m => m.estmatpr_estudiante_id == estudianteId)
                .ToListAsync();

            return [..consulta.Select(m => new Dominio.Entidades.Materia
            (
                m.estmatpr_profesor_materia?.profmat_materia_id ?? 0,
                m.estmatpr_profesor_materia?.profmat_materia?.mat_nombre ?? "",
                m.estmatpr_profesor_materia?.profmat_profesor_id ?? 0,
                m.estmatpr_profesor_materia?.profmat_profesor?.prof_nombre ?? "",
                m.estmatpr_profesor_materia?.profmat_materia?.mat_valor_creditos ?? 3
            ))];
        }

        public async Task<bool> GuardarMaterias(List<Dominio.Entidades.Materia> materias, int estudianteId)
        {
            if (materias == null || !materias.Any()) return false;

            List<Estudiante_Materia_Profesor> materiasAInscribir = [];

            foreach (var materia in materias)
            {
                // 1. Buscamos el ID de la relación
                int idMateriaProfesor = await contexto.Profesor_Materia
                    .Where(pm => pm.profmat_profesor_id == materia.ProfesorId && pm.profmat_materia_id == materia.Id)
                    .Select(pm => pm.profmat_id)
                    .FirstOrDefaultAsync();

                if (idMateriaProfesor == 0)
                {
                    throw new Exception($"La materia {materia.Id} no está asignada al profesor {materia.ProfesorId}");
                }

                Estudiante_Materia_Profesor materiaEstudiante = new Estudiante_Materia_Profesor()
                {
                    estmatpr_profesor_materia_id = idMateriaProfesor,
                    estmatpr_estudiante_id = estudianteId,
                    estmatpr_activo = 1
                };

                materiasAInscribir.Add(materiaEstudiante);
            }

            if (materiasAInscribir.Any())
            {
                await contexto.Estudiante_Materia_Profesors.AddRangeAsync(materiasAInscribir);
                await contexto.SaveChangesAsync();
            }

            return true;
        }

        private static readonly Expression<Func<Materia, Dominio.Entidades.Materia>> detallesMateria =
            m => new Dominio.Entidades.Materia
            (
                m.mat_id,
                m.mat_nombre ?? "",
                m.Profesor_Materia.Select(pm => pm.profmat_profesor_id).FirstOrDefault() ?? 0,
                m.Profesor_Materia.Select(pm => pm.profmat_profesor.prof_nombre).FirstOrDefault() ?? "",
                m.mat_valor_creditos ?? 3
            );
    }
}
