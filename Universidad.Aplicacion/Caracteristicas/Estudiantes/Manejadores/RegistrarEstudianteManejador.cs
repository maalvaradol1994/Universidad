using MediatR;
using Universidad.Aplicacion.Caracteristicas.Estudiantes.Comandos;
using Universidad.Dominio.Entidades;
using Universidad.Dominio.Repositorios;

namespace Universidad.Aplicacion.Caracteristicas.Estudiantes.Manejadores
{
    public class RegistrarEstudianteManejador(
        IEstudianteRepositorio estudianteRepositorio,
        IUsuarioRepositorio usuarioRepositorio,
        IProfesorRepositorio profesorRepositorio) : IRequestHandler<RegistrarEstudianteComando, int>
    {
        public async Task<int> Handle(RegistrarEstudianteComando request, CancellationToken cancellationToken)
        {
            int nuevoUsuarioId = 0;
            int? usuarioId = await usuarioRepositorio.AgregarUsuario(new Usuario(
                request.Correo, 
                request.Nombre, 
                request.Identificacion, 
                request.Clave,
                request.Rol));

            if( usuarioId == null)
            {
                throw new Exception("Error almacenando el usuario");
            }

            if( request.Rol == "Estudiante")
            {
                Estudiante nuevoEstudiante = new(request.Nombre, request.Correo, request.Clave, request.Identificacion, usuarioId ?? 0);
                await estudianteRepositorio.AgregarEstudiante(nuevoEstudiante);
                nuevoUsuarioId = nuevoEstudiante.Id;  
            }
            else if(request.Rol == "Profesor")
            {
                Profesor nuevoProfesor = new(request.Nombre, request.Identificacion, usuarioId ?? 0);
                await profesorRepositorio.AgregarProfesor(nuevoProfesor);
                nuevoUsuarioId = nuevoProfesor.Id;
            }

            return nuevoUsuarioId;
        }
    }
}
