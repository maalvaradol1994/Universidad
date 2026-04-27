using MediatR;
using Universidad.Aplicacion.Caracteristicas.Estudiantes.Comandos;
using Universidad.Dominio.Entidades;
using Universidad.Dominio.Repositorios;

namespace Universidad.Aplicacion.Caracteristicas.Estudiantes.Manejadores
{
    public class RegistrarEstudianteManejador(IEstudianteRepositorio estudianteRepositorio): IRequestHandler<RegistrarEstudianteComando, int>
    {
        public async Task<int> Handle(RegistrarEstudianteComando request, CancellationToken cancellationToken)
        {
            Estudiante nuevoEstudiante = new(request.Nombre, request.Correo, request.Clave, request.Identificacion);

            await estudianteRepositorio.AgregarEstudiante(nuevoEstudiante);

            return nuevoEstudiante.Id;
        }
    }
}
