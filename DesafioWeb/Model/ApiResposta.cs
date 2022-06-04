namespace WebDesafio.Models
{
    public class ApiResposta<T>
    {
        public T Data { get; set; }
        public bool? Sucesso { get; set; }
        public int StatusCode { get; set; }
        public string Mensagem { get; set; }

        public ApiResposta(T data, bool? sucesso, int statusCode, string mensagem)
        {
            Data = data;
            Sucesso = sucesso;
            StatusCode = statusCode;
            Mensagem = mensagem;
        }
    }
}
