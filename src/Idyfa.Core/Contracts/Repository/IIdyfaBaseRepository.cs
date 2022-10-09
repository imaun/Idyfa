namespace Idyfa.Core.Contracts.Repository;

public interface IIdyfaBaseRepository<TEntity, TKey> where TEntity : class
{

    void Add(TEntity entity);
    
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    void Update(TEntity entity);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    void Delete(TEntity entity);

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    TEntity FindById(TKey id);

    Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default);

    IReadOnlyCollection<TEntity> GetAll();

    Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
}