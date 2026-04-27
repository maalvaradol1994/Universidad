using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.Dominio.Entidades;

namespace Universidad.Dominio.Repositorios
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario?> ObtenerUsuarioPorNombreUsuario(string nombreUsuario);
    }
}
