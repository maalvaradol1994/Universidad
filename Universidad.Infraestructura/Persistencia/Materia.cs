using System;
using System.Collections.Generic;

namespace Universidad.Infraestructura.Persistencia;

public partial class Materia
{
    public int mat_id { get; set; }

    public string? mat_nombre { get; set; }

    public string? mat_codigo { get; set; }

    public short? mat_valor_creditos { get; set; }

    public short? mat_activo { get; set; }

    public virtual ICollection<Profesor_Materium> Profesor_Materia { get; set; } = new List<Profesor_Materium>();
}
