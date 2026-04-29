using Moq;
using Xunit;
using Universidad.Aplicacion.Caracteristicas.Estudiantes.Manejadores;
using Universidad.Aplicacion.Caracteristicas.Estudiantes.Consultas;
using Universidad.Aplicacion.Caracteristicas.Estudiantes.DTOS;
using Universidad.Dominio.Repositorios;
using Universidad.Dominio.Entidades;
using System.Collections.Generic;

namespace Universidad.Tests;

public class ObtenerEstudianteManejadorTests
{
    [Fact]
    public async Task Handle_WhenStudentExists_ReturnsEstudianteDto()
    {
        // Arrange
        var estudianteRepoMock = new Mock<IEstudianteRepositorio>();

        var materias = new List<Materia> { new Materia(1, "Matematicas", 2, "Profesor A") };
        var estudiante = new Estudiante(1, "Test User", "test@example.com", "pwd", "ID123", 1);

        // Manually set private collection using reflection if needed

        estudianteRepoMock.Setup(r => r.ObtenerEstudiantePorId(1)).ReturnsAsync(estudiante);

        var handler = new ObtenerEstudianteManejador(estudianteRepoMock.Object);

        var query = new ObtenerEstudiantePorIdQuery(1);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }
}
