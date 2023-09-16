namespace Prosigliere.Blog.Fakes;

public class FakeRepository<T> : IRepository<T> where T : IEntity
{
    private readonly List<T> _entities = new();
    private int _nextId = 1;

    public virtual void Add(T entity)
    {
        entity.Id = _nextId++;
        _entities.Add(entity);
    }

    public T? ById(int id) => 
        _entities.FirstOrDefault(entity => entity.Id == id);

    public IEnumerable<T> All() => _entities;
}