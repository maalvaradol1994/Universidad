using Universidad.Dominio.Excepciones;

namespace Universidad.Dominio.Entidades
{
    public class Materia
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public int Creditos { get; private set; }
        public int ProfesorId { get; private set; }
        public string ProfesorNombre { get; private set; }

        protected Materia()
        {
            Id = 0;
            Nombre = null!;
            Creditos = 0;
            ProfesorId = 0;
            ProfesorNombre = null!;
        }

        public Materia(int id, string nombre, int profesorId, string profesorNombre, int creditos = 3)
        {
            Id = id;
            ValidarNombre(nombre);
            Nombre = nombre;
            ProfesorId = profesorId;
            ProfesorNombre = profesorNombre;
            Creditos = creditos;
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
