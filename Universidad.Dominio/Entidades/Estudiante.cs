using Universidad.Dominio.Excepciones;

namespace Universidad.Dominio.Entidades;

public class Estudiante
{
    public int Id { get; private set; }
    public int UsuarioId { get; private set; }
    public string Nombre { get; private set; }
    public string Correo { get; private set; }
    public string Clave { get; private set; }
    public string Identificacion { get; private set; }
    public int CreditosDisponibles { get; private set; }

    private readonly List<Materia> materiasInscritas = [];
    public IReadOnlyCollection<Materia> MateriasInscritas => materiasInscritas.AsReadOnly();

    protected Estudiante()
    {
        UsuarioId = 0;
        Nombre = null!;
        Correo = null!;
        Clave = null!;
        Identificacion = null!;
    }

    public Estudiante(string nombre, string correo, string clave, string identificacion, int usuarioId)
    {
        UsuarioId = usuarioId;
        Nombre = nombre;
        Correo = correo;
        Clave = clave;
        CreditosDisponibles = 9;
        Identificacion = identificacion;
    }

    public Estudiante(int id, string nombre, string correo, string clave, string identificacion, int usuarioId)
        : this(nombre, correo, clave, identificacion, usuarioId)
    {
        Id = id;
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

        if (materias.Count > CantidadMaximaMaterias)
        {
            throw new ReglaNegocioExcepcion($"El estudiante debe seleccionar máximo {CantidadMaximaMaterias} materias.");
        }

        if (materias.Count == 0)
        {
            throw new ReglaNegocioExcepcion($"El estudiante debe seleccionar al menos 1 materia.");
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
