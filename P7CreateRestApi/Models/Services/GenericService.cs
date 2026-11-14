using AutoMapper;
using Dot.Net.WebApi.Models.Services.Interfaces;
using Dot.Net.WebApi.Repositories.Interfaces;

namespace Dot.Net.WebApi.Models.Services
{
    public class GenericService<Entity> : IGenericService<Entity>
        where Entity : class
    {
        protected readonly IGenericRepository<Entity> _repository;

        public GenericService(IGenericRepository<Entity> repository)
        {
            _repository = repository;
        }

        public int Add(Entity entity)
        {
            return _repository.Add(entity);
        }
        public Entity Get(int id)
        {
            return _repository.Get(id);
        }
        public void Update(Entity entity)
        {
            _repository.Update(entity);
        }
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
