using Universidad.Dominio.Excepciones;

namespace Universidad.Dominio.Entidades
{
    public class Materia
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public int Creditos { get; private set; }
        public int ProfesorId { get; private set; }

        protected Materia()
        {
            Nombre = null!;
        }

        public Materia(string nombre, int profesorId)
        {
            ValidarNombre(nombre);
            Nombre = nombre;
            ProfesorId = profesorId;
            Creditos = 3;
        }

        private static void ValidarNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ReglaNegocioExcepcion("El nombre de la materia es obligatorio.");
            }
        }
    }
}
