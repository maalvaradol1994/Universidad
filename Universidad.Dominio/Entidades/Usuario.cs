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
        public string Nombre { get; private set; }
        public string Identificacion { get; private set; }
        public string Clave { get; private set; }
        public string Rol { get; private set; }
        public int Activo { get; private set; }
        protected Usuario()
        {
            NombreUsuario = null!;
            Nombre = null!;
            Identificacion = null!;
            Clave = null!;
            Activo = 1;
            Rol = null!;       
        }
        public Usuario(string nombreUsuario, string nombre, string identificacion, string clave, string rol)
        {
            NombreUsuario = nombreUsuario;
            Nombre = nombre;
            Identificacion = identificacion;
            Clave = clave;
            Rol = rol;
        }
    }

}
