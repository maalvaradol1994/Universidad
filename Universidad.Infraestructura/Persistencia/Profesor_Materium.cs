using System;
using System.Collections.Generic;

namespace Universidad.Infraestructura.Persistencia;

public partial class Profesor_Materium
{
    public int profmat_id { get; set; }

    public int? profmat_profesor_id { get; set; }

    public int? profmat_materia_id { get; set; }

    public short? profmat_activo { get; set; }

    public virtual ICollection<Estudiante_Materia_Profesor> Estudiante_Materia_Profesors { get; set; } = new List<Estudiante_Materia_Profesor>();

    public virtual Materia? profmat_materia { get; set; }

    public virtual Profesore? profmat_profesor { get; set; }
}
