using Domain.Interfaces;
using EntitiesDesafio;
using Infra.Repository.Generics;
using Microsoft.Extensions.Logging;

namespace Infra.Repository.Repositories
{
    public class RepositoryPessoa : RepositoryGenerics<Pessoa>, IPessoa
    {
        public RepositoryPessoa(ILogger<RepositoryPessoa> logger)
        {
        }
    }
}
