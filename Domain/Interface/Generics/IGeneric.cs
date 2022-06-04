namespace Domain.Interfaces.Generics
{
    public interface IGeneric<T> where T : class
    {
        Task<int> Add(T Objeto);
        Task Update(T Objeto);
        Task Delete(T Objeto);
        Task<T> GetEntityById(int Id);
        Task<List<T>> List();
    }
}
