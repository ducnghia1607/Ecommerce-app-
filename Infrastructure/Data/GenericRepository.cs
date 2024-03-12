﻿using Core;
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

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(_context.Set<T>(), spec).CountAsync();
    }

    private IQueryable<T> ApplySpecification(IQueryable<T> query, ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(query, spec);
    }




    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Attach(entity);
        // Khi update thì phải sửa lại state của nó thành modified ; 
        _context.Entry<T>(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
}
