using System;
using System.Collections.Generic;

namespace Universidad.Infraestructura.Persistencia;

public partial class Estudiante_Materia_Profesor
{
    public int estmatpr_id { get; set; }

    public int? estmatpr_profesor_materia_id { get; set; }

    public short? estmatpr_activo { get; set; }

    public virtual Profesor_Materium? estmatpr_profesor_materia { get; set; }
}
