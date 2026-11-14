using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Repositories.Interfaces;

namespace Dot.Net.WebApi.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly LocalDbContext _context;
        public GenericRepository(LocalDbContext context)
        {
            _context = context;
        }

        public virtual int Add(T entity)
        {
            var entry = _context.Set<T>().Add(entity);
            _context.SaveChanges();

            var pk = entry.Metadata.FindPrimaryKey();
            if (pk != null && pk.Properties.Count > 0)
            {
                var keyName = pk.Properties[0].Name;
                var prop = entry.Property(keyName);
                if (prop != null && prop.CurrentValue != null)
                {
                    return Convert.ToInt32(prop.CurrentValue);
                }
            }

            return 0;
        }
        public virtual T Get(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity == null)
            {
                throw new InvalidOperationException($"L'entité de type {typeof(T).Name} avec l'identifiant {id} est introuvable.");
            }
            return entity;
        }
        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
        public virtual void Delete(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity == null)
                return;

            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
    }
}
