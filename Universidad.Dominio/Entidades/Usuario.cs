using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Universidad.Dominio.Entidades
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string NombreUsuario { get; private set; }
        public string Identificacion { get; private set; }
        public string Clave { get; private set; }
        public int Activo { get; private set; }
        protected Usuario()
        {
            NombreUsuario = null!;
            Identificacion = null!;
            Clave = null!;
            Activo = 1;
        
        }
        public Usuario(string nombreUsuario, string identificacion, string clave)
        {
            NombreUsuario = nombreUsuario;
            Identificacion = identificacion;
            Clave = clave;
        }
    }

}
