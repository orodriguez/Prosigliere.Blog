namespace Prosigliere.Blog;

public interface IRepository<T>
{
    void Add(T entity);
    T? ById(int id);
}