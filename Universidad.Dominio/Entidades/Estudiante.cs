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
}
