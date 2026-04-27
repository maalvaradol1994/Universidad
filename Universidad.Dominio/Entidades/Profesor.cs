namespace Universidad.Dominio.Entidades
{
    public class Profesor
    {
        public int Id { get; private set; }
        public int UsuarioId { get; private set; }
        public string Nombre { get; private set; }
        public string Identificacion { get; private set; }

        protected Profesor()
        {
            UsuarioId = 0;
            Nombre = null!;
            Identificacion = null!;
        }

        public Profesor(string nombre, string identificacion, int usuarioId)
        {
            Nombre = nombre;
            Identificacion = identificacion;
            UsuarioId = usuarioId;
        }
    }
}
