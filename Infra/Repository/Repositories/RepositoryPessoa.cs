using Domain.Interfaces;
using EntitiesDesafio;
using Infra.Repository.Generics;
using InfraTesteCandidato.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository.Repositories
{
    public class RepositoryPessoa : RepositoryGenerics<Pessoa>, IPessoa
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;
        public RepositoryPessoa()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        
    }
}
