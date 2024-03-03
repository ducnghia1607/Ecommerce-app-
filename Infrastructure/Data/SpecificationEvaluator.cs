using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
{

    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
    {

        var query = inputQuery;

        if (spec.Criteria != null)
        {
            // Criteria is a predicate 
            query = query.Where(spec.Criteria);
        }

        // query  : inital value 
        // current : current value 
        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

        return query;

    }

}
