using Domain.Interfaces;
using EntitiesDesafio;
using Infra.Repository.Generics;
using Microsoft.Extensions.Logging;

namespace Infra.Repository.Repositories
{
    public class RepositoryCidade : RepositoryGenerics<Cidade>, ICidade
    {
        public RepositoryCidade(ILogger<RepositoryCidade> logger)
        {
        }        
    }
}
