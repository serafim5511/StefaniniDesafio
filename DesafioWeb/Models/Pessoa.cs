namespace EntitiesDesafio
{
    public class Pessoa
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public int? Idade { get; set; }
        public int? Id_Cidade { get; set; }
#pragma warning disable CS0436 // Conflitos de tipo com o tipo importado
        public Cidade? Cidade { get; set; }
#pragma warning restore CS0436 // Conflitos de tipo com o tipo importado
    }
}
