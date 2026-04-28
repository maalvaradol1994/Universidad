using MediatR;
using Universidad.Dominio.Entidades;

namespace Universidad.Aplicacion.Caracteristicas.Materias.Comandos
{
    public record InscribirMateriasComando(string Usuario, List<Materia> Materias) : IRequest<bool>;
}
