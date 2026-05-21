using EnterpriseInventory.WPF.Infrastructure;
using EnterpriseInventory.WPF.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context = new();

    public List<T> GetAll() => _context.Set<T>().ToList();

    public T? GetById(int id) => _context.Set<T>().Find(id);

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
    }
}