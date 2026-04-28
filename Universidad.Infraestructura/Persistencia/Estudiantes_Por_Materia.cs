using System;
using System.Collections.Generic;

namespace Universidad.Infraestructura.Persistencia;

public partial class Estudiantes_Por_Materia
{
    public int id_materia { get; set; }

    public string? materia { get; set; }

    public short? valor_creditos { get; set; }

    public int id_estudiante { get; set; }

    public string estudiante_nombre { get; set; } = null!;

    public string? identificacion { get; set; }
}
