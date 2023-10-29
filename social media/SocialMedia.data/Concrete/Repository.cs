using Microsoft.EntityFrameworkCore;
using SocialMedia.data.Abstract;

namespace SocialMedia.data.Concrete
{
    public class Repository<TEntity, TContext> : IRepository<TEntity>
    where TContext:DbContext,new()
    where TEntity:class
    {
        public void Create(TEntity entity)
        {
            using(var context = new TContext())
            {
                context.Set<TEntity>().Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using(var context = new TContext())
            {
                context.Set<TEntity>().Remove(entity);
                context.SaveChanges();
            }
        }

        public void DeleteMultiple(List<TEntity> entities)
        {
            using(var context = new TContext())
            {
                context.Set<TEntity>().RemoveRange(entities);
                context.SaveChanges();
            }
        }

        public List<TEntity> GetAll()
        {
            using(var context = new TContext())
            {
                return context.Set<TEntity>().ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using(var context = new TContext())
            {
                context.Entry(entity).State=EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}