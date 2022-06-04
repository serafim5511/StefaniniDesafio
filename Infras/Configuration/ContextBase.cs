using EntitiesDesafio;
using Microsoft.EntityFrameworkCore;

namespace InfraTesteCandidato.Configuration
{
    public class ContextBase : DbContext
    {
#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
        {
        }

        public virtual DbSet<Pessoa> Pessoa { get; set; }
        public virtual DbSet<Cidade> Cidade { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"data source=DESKTOP-L8KS43S\SQLEXPRESS; initial catalog=Desafio;persist security info=True; Integrated Security=SSPI;",
                         options => options.EnableRetryOnFailure());
                base.OnConfiguring(optionsBuilder);
            }
        }
    }
}
