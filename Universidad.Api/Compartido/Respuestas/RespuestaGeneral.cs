namespace Universidad.Api.Compartido.Respuestas
{
    public class RespuestaGeneral<T>
    {
        public bool Exitoso { get; set; }
        public string? Mensaje { get; set; }
        public T? Resultado { get; set; }
        public int StatusCode { get; set; }
    }
}
