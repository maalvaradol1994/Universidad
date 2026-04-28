using System;
using System.Collections.Generic;

namespace Universidad.Infraestructura.Persistencia;

public partial class Usuario
{
    public int usu_id { get; set; }

    public string usu_usuario { get; set; } = null!;

    public string usu_clave { get; set; } = null!;

    public string usu_identificacion { get; set; } = null!;

    public short? usu_activo { get; set; }

    public string usu_rol { get; set; } = null!;

    public virtual Estudiante? Estudiante { get; set; }

    public virtual Profesore? Profesore { get; set; }
}
