using MediatR;

namespace Universidad.Aplicacion.Caracteristicas.Materias.Comandos
{
    public record InscribirMateriasComando(int EstudianteId, List<int> MateriasIds) : IRequest<bool>;
}
