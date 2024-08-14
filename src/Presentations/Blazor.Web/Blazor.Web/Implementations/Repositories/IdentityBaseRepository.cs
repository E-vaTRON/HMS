using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Blazor.Web;

public class IdentityBaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly IdentityDbContext _context;
    protected readonly DbSet<T> _dbSet;
    public IdentityBaseRepository(IdentityDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public virtual List<T> FullTextSearch(Expression<Func<T, string>> predicate, string search)
    { 
        var searchQuery = search.Split().Aggregate((s1, s2) => "\"" + s1 + "\"" + " AND " + "\"" + s2 + "\""); 
        return _dbSet.Where(x => EF.Functions.Contains(predicate.Body, searchQuery)).ToList();
    } 

    public virtual IQueryable<T> FindAll(Expression<Func<T, bool>>? predicate = null)
        => _dbSet.WhereIf(predicate != null, predicate!);

    public virtual async Task<T?> FindByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object[] { Guid.Parse(id) }, cancellationToken);
    }

    public virtual async Task<T?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(name, cancellationToken);
    }

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        _dbSet.AddRange(entities);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}