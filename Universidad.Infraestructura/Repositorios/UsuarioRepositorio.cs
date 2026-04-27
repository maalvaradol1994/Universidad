using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.Dominio.Entidades;
using Universidad.Dominio.Repositorios;
using Universidad.Infraestructura.Persistencia;

namespace Universidad.Infraestructura.Repositorios
{
    public class UsuarioRepositorio(UniversidadContexto contexto) : IUsuarioRepositorio
    {
        async Task<Universidad.Dominio.Entidades.Usuario?> IUsuarioRepositorio.ObtenerUsuarioPorNombreUsuario(string nombreUsuario)
        {
            return await contexto.Usuarios
                .Where(usuario => usuario.usu_usuario == nombreUsuario)
                .Select(u => new Dominio.Entidades.Usuario(u.usu_usuario, u.usu_identificacion, u.usu_clave))
                .FirstOrDefaultAsync();
        }
    }
}
