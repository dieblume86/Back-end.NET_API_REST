namespace Dot.Net.WebApi.Models.Services.Interfaces
{
    public interface IGenericService<Entity>
        where Entity : class
    {
        int Add(Entity entity);
        Entity Get(int id);

        void Update(Entity entity);

        void Delete(int id);
    }
}