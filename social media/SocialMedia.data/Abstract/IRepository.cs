namespace SocialMedia.data.Abstract
{
    public interface IRepository<T>
    {
        void Create(T entity);
        void Delete(T entity);
        void DeleteMultiple(List<T> entities);
        void Update(T entity);
        List<T> GetAll();
        
        
    }
}