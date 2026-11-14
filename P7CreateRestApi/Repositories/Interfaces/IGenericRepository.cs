namespace Dot.Net.WebApi.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        int Add(T entity);
        T Get(int id);
        void Update(T entity);
        void Delete(int id);
    }
}
