using Universidad.Aplicacion.Caracteristicas.Materias.DTOS;

namespace Universidad.Aplicacion.Caracteristicas.Estudiantes.DTOS;

public record EstudianteDto(int Id, string Nombre, string Correo, int CreditosDisponibles, List<MateriaDto> MateriasInscritas);