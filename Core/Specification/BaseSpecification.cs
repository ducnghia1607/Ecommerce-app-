using System.Linq.Expressions;
using Core;

namespace Infrastructure;

public class BaseSpecification<T> : ISpecification<T>
{
    public BaseSpecification()
    {
    }

    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<T, bool>> Criteria { get; } //  Func<T,bool> = (p => return p.ProductTypeId == typeId ) 

    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

    public Expression<Func<T, object>> OrderBy { get; private set; }

    public Expression<Func<T, object>> OrderByDescending { get; private set; }

    public int Skip { get; private set; }

    public int Take { get; private set; }

    public bool isPagingEnabled { get; private set; }

    protected void AddInClude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;

    }

    public void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
    }

    public void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        isPagingEnabled = true;
    }
}
