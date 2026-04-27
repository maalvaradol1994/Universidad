using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Universidad.Api.Compartido.Respuestas;
using Universidad.Aplicacion.Caracteristicas.Estudiantes.Comandos;
using Universidad.Dominio.Entidades;
using Universidad.Dominio.Repositorios;

namespace Universidad.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IConfiguration configuracion, IUsuarioRepositorio usuarioRepositorio) : ControllerBase
{
    public record LoginRequest(string Usuario, string Password);
    public record RegisterRequest(string Nombre, string Correo, string Password, string Identificacion);

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, [FromServices] IEstudianteRepositorio repositorioEstudiantes)
    {
        Usuario? usuario = await usuarioRepositorio.ObtenerUsuarioPorNombreUsuario(request.Usuario);

        if (usuario != null && usuario.Clave == request.Password)
        {
            string token = GenerarToken(usuario.NombreUsuario, "Estudiante", usuario.NombreUsuario);
            return Ok(new { Token = token });
        }

        return Unauthorized(new { Error = "Credenciales incorrectas" });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request,
        [FromServices] IEstudianteRepositorio repositorio,
        [FromServices] IMediator mediator)
    {
        Usuario? usuarioExistente = await usuarioRepositorio.ObtenerUsuarioPorNombreUsuario(request.Correo);

        if (usuarioExistente is not null)
        {
            return Conflict(new { Error = "Ya existe un usuario registrado con ese correo" });
        }

        RegistrarEstudianteComando comando = new(request.Nombre, request.Correo, request.Password, request.Identificacion);
        int estudianteId = await mediator.Send(comando);

        RespuestaGeneral<int> respuesta = new()
        {
            Exitoso = true,
            Mensaje = "Usuario registrado con éxito",
            Resultado = estudianteId,
            StatusCode = StatusCodes.Status201Created
        };

        return StatusCode(StatusCodes.Status201Created, respuesta);
    }

    private string GenerarToken(string correo, string rol, string nombre)
    {
        SymmetricSecurityKey llaveSecreta = new(Encoding.UTF8.GetBytes(configuracion["Jwt:Key"]!));
        SigningCredentials credenciales = new(llaveSecreta, SecurityAlgorithms.HmacSha256);

        Claim[] claims =
        {
            new(JwtRegisteredClaimNames.Sub, correo),
            new(ClaimTypes.Role, rol),
            new(ClaimTypes.Name, nombre),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        JwtSecurityToken token = new(
            issuer: configuracion["Jwt:Issuer"],
            audience: configuracion["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: credenciales);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
