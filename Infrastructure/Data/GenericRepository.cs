using Core;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly StoreContext _context;
    public GenericRepository(StoreContext context)
    {
        _context = context;

    }
    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }



    public async Task<IReadOnlyList<T>> ListAllSync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
    {
        return await ApplySpecification(_context.Set<T>(), spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(_context.Set<T>(), spec).ToListAsync();
    }

    private IQueryable<T> ApplySpecification(IQueryable<T> query, ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(query, spec);
    }
}
