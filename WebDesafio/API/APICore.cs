using Domain.DTOs.Pessoa;
using Newtonsoft.Json;
using RestSharp;

namespace WebDesafio.API
{
    public static class APICore
    {
        public static Pessoa ApiGet(int id)
        {
            var request = new RestRequest("api/BuscarMotorista?id=" + id, Method.GET);

            return JsonConvert.DeserializeObject<Pessoa>(RestClient().Execute(request).Content);
        }
        public static List<Pessoa> ApiGetAll()
        {
            var request = new RestRequest("api/BuscarLista", Method.GET);

            var queryResult = RestClient().Execute(request);

            return JsonConvert.DeserializeObject<List<Pessoa>>(queryResult.Content);
        }

        public static void ApiPost(Object dados)
        {
            var request = new RestRequest("api/Cadastrar", Method.POST);
            request.AddJsonBody(dados);
            RestClient().Execute(request);
        }
        public static bool ApiPostEdit(Object dados)
        {
            var request = new RestRequest("api/Atualizar", Method.POST);
            request.AddJsonBody(dados);
            return RestClient().Execute(request).IsSuccessful == true? true: false ;
        }
        public static bool ApiDelete(Object motorista)
        {
            var request = new RestRequest("api/Delete", Method.DELETE);
            request.AddJsonBody(motorista);
            return RestClient().Execute(request).IsSuccessful == true ? true : false;
        }
        private static RestClient RestClient() {
            return new RestClient("https://localhost:44347/");
        }

    }
}
