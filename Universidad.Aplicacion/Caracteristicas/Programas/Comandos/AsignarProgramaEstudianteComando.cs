using MediatR;

namespace Universidad.Aplicacion.Caracteristicas.Programas.Comandos;

public record AsignarProgramaEstudianteComando(string NombreUsuario, int ProgramaId) : IRequest<bool>;
