namespace Prosigliere.Blog.Tests;

public class FakeRepository<T> : IRepository<T> where T : IEntity
{
    private readonly List<T> _entities = new List<T>();
    private int _nextId = 1;

    public void Add(T entity)
    {
        entity.Id = _nextId++;
        _entities.Add(entity);
    }
}