using System;
using System.Collections.Generic;

namespace Universidad.Infraestructura.Persistencia;

public partial class Estudiante
{
    public int est_id { get; set; }

    public string est_nombre { get; set; } = null!;

    public short? est_activo { get; set; }

    public string est_identificacion { get; set; } = null!;

    public int? est_usuario_id { get; set; }

    public virtual ICollection<Estudiante_Materia_Profesor> Estudiante_Materia_Profesors { get; set; } = new List<Estudiante_Materia_Profesor>();

    public virtual ICollection<Estudiante_Programa> Estudiante_Programas { get; set; } = new List<Estudiante_Programa>();

    public virtual Usuario? Usuario { get; set; }

    public virtual Usuario? est_usuario { get; set; }
}
