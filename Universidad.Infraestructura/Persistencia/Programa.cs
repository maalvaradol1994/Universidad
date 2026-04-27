using System;
using System.Collections.Generic;

namespace Universidad.Infraestructura.Persistencia;

public partial class Programa
{
    public int prog_id { get; set; }

    public string? prog_nombre { get; set; }

    public short? prog_total_creditos { get; set; }

    public short? prog_activo { get; set; }

    public virtual ICollection<Estudiante_Programa> Estudiante_Programas { get; set; } = new List<Estudiante_Programa>();
}
