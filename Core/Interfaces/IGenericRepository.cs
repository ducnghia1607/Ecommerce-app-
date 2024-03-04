namespace Core;

public interface IGenericRepository<T> where T : BaseEntity
{

    Task<T> GetByIdAsync(int id);

    Task<IReadOnlyList<T>> ListAllSync();

    Task<T> GetEntityWithSpec(ISpecification<T> spec);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

    Task<int> CountAsync(ISpecification<T> spec);
}
