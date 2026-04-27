using MediatR;

namespace Universidad.Aplicacion.Caracteristicas.Estudiantes.Comandos
{
    public record RegistrarEstudianteComando(string Nombre, string Correo, string Clave, string Identificacion) : IRequest<int>;
}
