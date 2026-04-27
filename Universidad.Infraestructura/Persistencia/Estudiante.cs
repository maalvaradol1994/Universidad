using System;
using System.Collections.Generic;

namespace Universidad.Infraestructura.Persistencia;

public partial class Estudiante
{
    public int est_id { get; set; }

    public string est_nombre { get; set; } = null!;

    public short? est_activo { get; set; }

    public string? est_identificacion { get; set; }

    public virtual ICollection<Estudiante_Programa> Estudiante_Programas { get; set; } = new List<Estudiante_Programa>();
}
