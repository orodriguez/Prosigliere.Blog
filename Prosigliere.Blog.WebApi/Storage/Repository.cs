using Prosigliere.Blog.Entities;

namespace Prosigliere.Blog.WebApi.Storage;

public class Repository<T> : IRepository<T> where T : class, IEntity
{
    protected readonly ProsigliereBlogDbContext _db;

    public Repository(ProsigliereBlogDbContext db) => _db = db;

    public void Add(T entity)
    {
        _db.Set<T>().Add(entity);
        _db.SaveChanges();
    }

    T? IRepository<T>.ById(int id) => 
        _db.Set<T>().FirstOrDefault(entity => entity.Id == id);

    public IEnumerable<T> All() => 
        _db.Set<T>();
}