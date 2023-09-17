using Prosigliere.Blog.Entities;

namespace Prosigliere.Blog.WebApi.Storage;

public class Repository<T> : IRepository<T> where T : class, IEntity
{
    protected readonly ProsigliereBlogDbContext Db;
    
    public Repository(ProsigliereBlogDbContext db) => Db = db;

    public void Add(T entity)
    {
        Db.Set<T>().Add(entity);
        Db.SaveChanges();
    }

    public virtual T? ById(int id) => 
        Db.Set<T>().FirstOrDefault(entity => entity.Id == id);

    public IEnumerable<T> All() => 
        Db.Set<T>();
}