using MediatR;
using Universidad.Aplicacion.Caracteristicas.Estudiantes.Consultas;
using Universidad.Aplicacion.Caracteristicas.Estudiantes.DTOS;
using Universidad.Aplicacion.Caracteristicas.Materias.DTOS;
using Universidad.Dominio.Entidades;
using Universidad.Dominio.Excepciones;
using Universidad.Dominio.Repositorios;

namespace Universidad.Aplicacion.Caracteristicas.Estudiantes.Manejadores
{
    public class ObtenerEstudianteManejador(IEstudianteRepositorio estudianteRepositorio): IRequestHandler<ObtenerEstudiantePorIdQuery, EstudianteDto>
    {
        public async Task<EstudianteDto> Handle(ObtenerEstudiantePorIdQuery request, CancellationToken cancellationToken)
        {
            Estudiante? estudiante = await estudianteRepositorio.ObtenerEstudiantePorId(request.Id) ?? throw new ReglaNegocioExcepcion($"No se encontró ningún estudiante con el ID {request.Id}.");

            List<MateriaDto> materiasDto = [.. estudiante.MateriasInscritas.Select(m => new MateriaDto(m.Id, m.Nombre, m.Creditos, m.ProfesorId, m.ProfesorNombre))];

            return new EstudianteDto(
                estudiante.Id,
                estudiante.Nombre,
                estudiante.Correo,
                estudiante.CreditosDisponibles,
                materiasDto
            );
        }
    }
}
