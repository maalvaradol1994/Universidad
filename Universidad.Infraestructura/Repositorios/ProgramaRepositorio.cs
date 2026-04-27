using Microsoft.EntityFrameworkCore;
using Universidad.Dominio.Entidades;
using Universidad.Dominio.Repositorios;
using Universidad.Infraestructura.Persistencia;

namespace Universidad.Infraestructura.Repositorios
{
    public class ProgramaRepositorio(UniversidadContexto contexto) : IProgramaRepositorio
    {
        public async Task<List<Programa>> ObtenerTodos()
        {
            return await contexto.Programas
                .Where(programa => programa.prog_activo == 1)
                .Select(programa => new Programa(
                    programa.prog_id,
                    programa.prog_nombre!,
                    programa.prog_total_creditos ?? 0))
                .ToListAsync();
        }

        public async Task<Programa?> ObtenerPorId(int id)
        {
            return await contexto.Programas
                .Where(programa => programa.prog_id == id && programa.prog_activo == 1)
                .Select(programa => new Programa(
                    programa.prog_id,
                    programa.prog_nombre!,
                    programa.prog_total_creditos ?? 0))
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ExisteInscripcion(int estudianteId, int programaId)
        {
            return await contexto.Estudiante_Programas.AnyAsync(inscripcion =>
                inscripcion.estprog_estudiante_id == estudianteId &&
                inscripcion.estprog_programa_id == programaId &&
                inscripcion.estprog_activo == 1);
        }

        public async Task AsignarEstudianteAPrograma(int estudianteId, int programaId)
        {
            Estudiante_Programa nuevaInscripcion = new()
            {
                estprog_estudiante_id = estudianteId,
                estprog_programa_id = programaId,
                estprog_total_creditos = 0,
                estprog_activo = 1
            };

            await contexto.Estudiante_Programas.AddAsync(nuevaInscripcion);
            await contexto.SaveChangesAsync();
        }
    }
}
