using System;
using System.Collections.Generic;

namespace Universidad.Infraestructura.Persistencia;

public partial class Profesore
{
    public int prof_id { get; set; }

    public string prof_nombre { get; set; } = null!;

    public short? prof_activo { get; set; }

    public string? prof_identificacion { get; set; }

    public int? prof_usuario_id { get; set; }

    public virtual ICollection<Profesor_Materium> Profesor_Materia { get; set; } = new List<Profesor_Materium>();

    public virtual Usuario? prof_usuario { get; set; }
}
