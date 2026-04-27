using System;
using System.Collections.Generic;

namespace Universidad.Infraestructura.Persistencia;

public partial class Estudiante_Programa
{
    public int estprog_id { get; set; }

    public int? estprog_estudiante_id { get; set; }

    public int? estprog_programa_id { get; set; }

    public int? estprog_total_creditos { get; set; }

    public short? estprog_activo { get; set; }

    public virtual Estudiante? estprog_estudiante { get; set; }

    public virtual Programa? estprog_programa { get; set; }
}
