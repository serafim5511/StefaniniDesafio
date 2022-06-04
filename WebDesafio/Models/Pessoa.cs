using Newtonsoft.Json;
using NSwag.Annotations;
using System.Text.Json.Serialization;

namespace EntitiesDesafio
{
    public class Pessoa
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public int? Idade { get; set; }
        public int? Id_Cidade { get; set; }
        public Cidade? Cidade { get; set; }
    }
}
