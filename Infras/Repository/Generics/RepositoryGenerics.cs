using Domain.Interfaces.Generics;
using InfraTesteCandidato.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository.Generics
{
    public class RepositoryGenerics<T> : IGeneric<T> where T : class
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public RepositoryGenerics()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<int> Add(T Objeto)
        {           
            using (var data = new ContextBase(_OptionsBuilder))
            {                    
                await data.Set<T>().AddAsync(Objeto);
                await data.SaveChangesAsync();
                return (int)Objeto.GetType().GetProperty("Id").GetValue(Objeto,null);
            }            
        }

        public async Task Delete(T Objeto)
        {
            using (var data = new ContextBase(_OptionsBuilder))
            {
                data.Set<T>().Remove(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task<T> GetEntityById(int Id)
        {
            using (var data = new ContextBase(_OptionsBuilder))
            {
                return await data.Set<T>().FindAsync(Id);
            }
        }

        public async Task<List<T>> List()
        {
            using (var data = new ContextBase(_OptionsBuilder))
            {
                return await data.Set<T>().AsNoTracking().ToListAsync();
            }
        }

        public async Task Update(T Objeto)
        {
            using (var data = new ContextBase(_OptionsBuilder))
            {
                data.Set<T>().Update(Objeto);
                await data.SaveChangesAsync();
            }
        }
    }
}
