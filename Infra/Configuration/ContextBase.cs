using EntitiesDesafio;
using Microsoft.EntityFrameworkCore;

namespace InfraTesteCandidato.Configuration
{
    public class ContextBase : DbContext
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {
        }

        public virtual DbSet<Pessoa> Pessoa { get; set; }
        public virtual DbSet<Cidade> Cidade { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=Cache;Integrated Security=False;Encrypt=False;TrustServerCertificate=False");
                base.OnConfiguring(optionsBuilder);
            }
        }
    }
}
