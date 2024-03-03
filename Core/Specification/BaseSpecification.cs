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


    protected void AddInClude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

}
