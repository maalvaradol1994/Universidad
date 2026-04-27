namespace Universidad.Dominio.Entidades
{
    public class Profesor
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }

        protected Profesor()
        {
            Nombre = null!;
        }

        public Profesor(string nombre)
        {
            Nombre = nombre;
        }
    }
}
