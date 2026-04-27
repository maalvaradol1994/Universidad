using Universidad.Dominio.Excepciones;

namespace Universidad.Dominio.Entidades;

public class Estudiante
{
    public int Id { get; private set; }
    public string Nombre { get; private set; }
    public string Correo { get; private set; }
    public string Clave { get; private set; }
    public string Identificacion { get; private set; }
    public int CreditosDisponibles { get; private set; }

    private readonly List<Materia> materiasInscritas = [];
    public IReadOnlyCollection<Materia> MateriasInscritas => materiasInscritas.AsReadOnly();

    protected Estudiante()
    {
        Nombre = null!;
        Correo = null!;
        Clave = null!;
        Identificacion = null!;
    }

    public Estudiante(string nombre, string correo, string clave, string identificacion)
    {
        Nombre = nombre;
        Correo = correo;
        Clave = clave;
        CreditosDisponibles = 9;
        Identificacion = identificacion;
    }

    public void InscribirMaterias(List<Materia> materiasAInscribir)
    {
        ValidarCantidadMaterias(materiasAInscribir);
        ValidarProfesoresDiferentes(materiasAInscribir);
        ValidarCreditosSuficientes(materiasAInscribir);

        foreach (Materia materia in materiasAInscribir)
        {
            materiasInscritas.Add(materia);
            CreditosDisponibles -= materia.Creditos;
        }
    }

    private static void ValidarCantidadMaterias(List<Materia> materias)
    {
        const int CantidadMaximaMaterias = 3;

        if (materias.Count != CantidadMaximaMaterias)
        {
            throw new ReglaNegocioExcepcion($"El estudiante debe seleccionar exactamente {CantidadMaximaMaterias} materias.");
        }
    }

    private static void ValidarProfesoresDiferentes(List<Materia> materias)
    {
        IEnumerable<int> profesoresIds = materias.Select(materia => materia.ProfesorId).Distinct();

        if (profesoresIds.Count() != materias.Count)
        {
            throw new ReglaNegocioExcepcion("No puedes seleccionar materias dictadas por el mismo profesor.");
        }
    }

    private void ValidarCreditosSuficientes(List<Materia> materias)
    {
        int creditosRequeridos = materias.Sum(materia => materia.Creditos);

        if (creditosRequeridos > CreditosDisponibles)
        {
            throw new ReglaNegocioExcepcion($"Créditos insuficientes. Necesitas {creditosRequeridos} créditos, pero solo tienes {CreditosDisponibles} disponibles.");
        }
    }
}