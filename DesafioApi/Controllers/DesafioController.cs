using ApiDesafio.Models;
using Domain.DTOs.Pessoa;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ApplicationInsights.Query.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace ApiDesafio.Controllers
{
    [ApiVersion("1.0")]
    [Route("desafioapi/")]
    [Produces("application/json")]

    [SwaggerResponse((int)HttpStatusCode.NoContent, Type = typeof(ErrorResponse), Description = "O recurso solicitado não existe ou não foi localizado.")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse), Description = "A requisição foi malformada, omitindo atributos obrigatórios.")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponse), Description = "O recurso solicitado não existe ou não foi implementado")]

    public class DesafioController : Controller
    {
        private readonly IPessoa _pessoa;
        private readonly ICidade _cidade;

        public DesafioController(IPessoa Pessoa, ICidade Cidade)
        {
            _pessoa = Pessoa;
            _cidade = Cidade;
        }

        [HttpPost("pessoa")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Pessoa), Description = "Cadastrar na base os dados da Pessoa e Cidade.")]
        public async Task<IActionResult> Cadastrar([FromBody] Pessoa? pessoa)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, $"A requisição foi malformada, omitindo atributos obrigatórios.");

#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
                var id = await _cidade.Add(new EntitiesDesafio.Cidade() { Nome = pessoa.Cidade.Nome, UF = pessoa.Cidade.UF });
#pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.
                await _pessoa.Add(new EntitiesDesafio.Pessoa() { Id_Cidade = id, Nome = pessoa.Nome, Idade = pessoa.Idade, Cpf = pessoa.CPF });

                return Ok(new ApiResposta<DateTime>(DateTime.Now, true, 200, "Ok"));
            }
            catch (HttpStatusCodeException ex)
            {
                return StatusCode((int)ex.StatusCode, new ApiResposta<DateTime>(DateTime.Now, false, (int)ex.StatusCode, ex.Message));
            }
        }

        [HttpGet("/api/BuscarLista")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Pessoa), Description = "Buscar na base lista de dados.")]
        public async Task<IActionResult> BuscarLista()
        {
            try
            {
                List<Pessoa> pessoa = new List<Pessoa>();   

                var result = await _pessoa.List();
                foreach (var item in result)
                {
                    var cidade = await _cidade.GetEntityById(item.Id_Cidade.Value);
                    pessoa.Add(new Pessoa()
                    {
                        Id = item.Id,  
                        Idade = item.Idade,
                        Nome = item.Nome,
                        CPF = item.Cpf,
                        Cidade = new Cidade() { Nome = cidade.Nome , UF = cidade.UF}
                    });
                }
                return Ok(new ApiResposta<List<Domain.DTOs.Pessoa.Pessoa>>(pessoa, true, 200, "Ok"));
            }
            catch (HttpStatusCodeException ex)
            {
                return StatusCode((int)ex.StatusCode, new ApiResposta<DateTime>(DateTime.Now, false, (int)ex.StatusCode, ex.Message));
            }
        }

        [HttpGet("/api/BuscarPorId")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Pessoa), Description = "Buscar na base os dados utilisando Id.")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            try
            {
                var itens = await _pessoa.GetEntityById(id);
                if (itens is null)
                    throw new HttpStatusCodeException(System.Net.HttpStatusCode.NotFound, "O recurso solicitado não existe ou não foi implementado.");
                
                var cidade = await _cidade.GetEntityById(itens.Id_Cidade.Value);

                var pessoa = new Pessoa()
                {
                    Id = itens.Id,
                    Idade = itens.Idade,
                    Nome = itens.Nome,
                    CPF = itens.Cpf,
                    Cidade = new Cidade() { Nome = cidade.Nome, UF = cidade.UF }
                };
                return Ok(new ApiResposta<Domain.DTOs.Pessoa.Pessoa>(pessoa, true, 200, "Ok"));
            }
            catch (HttpStatusCodeException ex)
            {
                return StatusCode((int)ex.StatusCode, new ApiResposta<DateTime>(DateTime.Now, false, (int)ex.StatusCode, ex.Message));
            }
        }

        [HttpPut("/api/Atualizar/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Pessoa), Description = "Atualizar dados que consta na base utilisando Id e objeto Pessoa.")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Pessoa pessoa)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, $"A requisição foi malformada, omitindo atributos obrigatórios.");

                var itens = await _pessoa.GetEntityById(id);
                if (itens is null || itens.Id_Cidade is null)
                    throw new HttpStatusCodeException(System.Net.HttpStatusCode.NotFound, "O recurso solicitado não existe ou não foi implementado.");

                await _pessoa.Update(new EntitiesDesafio.Pessoa()
                {
                    Id = id,
                    Nome = pessoa.Nome,
                    Idade = pessoa.Idade,
                    Cpf = pessoa.CPF,
                    Id_Cidade = itens.Id_Cidade
                });

                await _cidade.Update(new EntitiesDesafio.Cidade()
                {
                    Id = itens.Id_Cidade.Value,
                    Nome = pessoa?.Cidade?.Nome,
                    UF = pessoa?.Cidade?.UF
                });

                return Ok(new ApiResposta<DateTime>(DateTime.Now, true, 200, "Ok"));
            }
            catch (HttpStatusCodeException ex)
            {
                return StatusCode((int)ex.StatusCode, new ApiResposta<DateTime>(DateTime.Now, false, (int)ex.StatusCode, ex.Message));
            }
        }
        
        [HttpDelete("/api/Delete/{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(Pessoa), Description = "Deletar dados que consta na base utilisando Id.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, $"A requisição foi malformada, omitindo atributos obrigatórios.");

                var itens = await _pessoa.GetEntityById(id);
                if (itens is null)
                    throw new HttpStatusCodeException(System.Net.HttpStatusCode.NotFound, "O recurso solicitado não existe ou não foi implementado.");

                await _pessoa.Delete(new EntitiesDesafio.Pessoa()
                {
                    Id = id
                });

                if (itens.Id_Cidade != null)
                {
                    await _cidade.Delete(new EntitiesDesafio.Cidade()
                    {
                        Id = itens.Id_Cidade.Value
                    });
                }

                return Ok(new ApiResposta<DateTime>(DateTime.Now, true, 200, "Ok"));
            }
            catch (HttpStatusCodeException ex)
            {
                return StatusCode((int)ex.StatusCode, new ApiResposta<DateTime>(DateTime.Now, false, (int)ex.StatusCode, ex.Message));
            }
        }
    }
}
