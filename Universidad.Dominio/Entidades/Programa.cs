using Universidad.Dominio.Excepciones;

namespace Universidad.Dominio.Entidades
{
    public class Programa
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public int TotalCreditos { get; private set; }

        protected Programa()
        {
            Nombre = null!;
        }

        public Programa(int id, string nombre, int totalCreditos)
        {
            ValidarNombre(nombre);

            Id = id;
            Nombre = nombre;
            TotalCreditos = totalCreditos;
        }

        private static void ValidarNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ReglaNegocioExcepcion("El nombre del programa es obligatorio.");
            }
        }
    }
}
