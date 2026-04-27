using MediatR;
using Universidad.Aplicacion.Caracteristicas.Estudiantes.Comandos;
using Universidad.Dominio.Entidades;
using Universidad.Dominio.Repositorios;

namespace Universidad.Aplicacion.Caracteristicas.Estudiantes.Manejadores
{
    public class RegistrarEstudianteManejador(IEstudianteRepositorio estudianteRepositorio, IUsuarioRepositorio usuarioRepositorio): IRequestHandler<RegistrarEstudianteComando, int>
    {
        public async Task<int> Handle(RegistrarEstudianteComando request, CancellationToken cancellationToken)
        {

            int? usuarioId = await usuarioRepositorio.AgregarUsuario(new Usuario(request.Correo, request.Nombre, request.Identificacion, request.Clave));

            if( usuarioId == null)
            {
                throw new Exception("Error almacenando el usuario");
            }

            Estudiante nuevoEstudiante = new(request.Nombre, request.Correo, request.Clave, request.Identificacion, usuarioId ?? 0);
            await estudianteRepositorio.AgregarEstudiante(nuevoEstudiante);

            return nuevoEstudiante.Id;

        }
    }
}
