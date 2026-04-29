using Moq;
using Xunit;
using Universidad.Aplicacion.Caracteristicas.Estudiantes.Manejadores;
using Universidad.Aplicacion.Caracteristicas.Estudiantes.Comandos;
using Universidad.Dominio.Repositorios;
using Universidad.Dominio.Entidades;

namespace Universidad.Tests;

public class RegistrarEstudianteManejadorTests
{
    [Fact]
    public async Task Handle_WhenRegisteringEstudiante_ReturnsNewId()
    {
        // Arrange
        var estudianteRepoMock = new Mock<IEstudianteRepositorio>();
        var usuarioRepoMock = new Mock<IUsuarioRepositorio>();
        var profesorRepoMock = new Mock<IProfesorRepositorio>();

        usuarioRepoMock.Setup(r => r.AgregarUsuario(It.IsAny<Usuario>())).ReturnsAsync(1);
        estudianteRepoMock.Setup(r => r.AgregarEstudiante(It.IsAny<Estudiante>())).Returns(Task.CompletedTask).Callback<Estudiante>(e => e.GetType());

        var handler = new RegistrarEstudianteManejador(estudianteRepoMock.Object, usuarioRepoMock.Object, profesorRepoMock.Object);

        var comando = new RegistrarEstudianteComando("test@example.com", "Test User", "123456", "password", "Estudiante");

        // Act
        var result = await handler.Handle(comando, CancellationToken.None);

        // Assert
        Assert.True(result > 0);
    }
}
