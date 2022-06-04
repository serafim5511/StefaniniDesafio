using Domain.DTOs.Pessoa;
using Newtonsoft.Json;
using RestSharp;
using WebDesafio.Models;

namespace WebDesafio.API
{
    public static class APICore
    {
        public static ApiResposta<Pessoa> ApiGet(int id)
        {
            var request = new RestRequest("api/BuscarPorId?id=" + id, Method.GET);
            return JsonConvert.DeserializeObject<ApiResposta<Pessoa>>(RestClient().Execute(request).Content);
        }
        public static ApiResposta<List<EntitiesDesafio.Pessoa>> ApiGetAll()
        {
            var request = new RestRequest("api/BuscarLista", Method.GET);

            var queryResult = RestClient().Execute(request);

            return JsonConvert.DeserializeObject<ApiResposta<List<EntitiesDesafio.Pessoa>>>(queryResult.Content);
        }

        public static void ApiPost(Object dados)
        {
            var request = new RestRequest("desafioapi/pessoa", Method.POST);
            request.AddJsonBody(dados);
            request.AddHeader("Accept", "application/json");
            RestClient().Execute(request);
        }
        public static bool ApiPostEdit(int id, Object dados)
        {
            var request = new RestRequest("api/Atualizar/" + id, Method.PUT);
            request.AddJsonBody(dados);
            return RestClient().Execute(request).IsSuccessful == true ? true : false;
        }
        public static bool ApiDelete(int id)
        { 
            var request = new RestRequest("api/Delete/"+ id, Method.DELETE);
            return RestClient().Execute(request).IsSuccessful == true ? true : false;
        }
        private static RestClient RestClient()
        {
            return new RestClient("https://localhost:7289/");
        }

    }
}
