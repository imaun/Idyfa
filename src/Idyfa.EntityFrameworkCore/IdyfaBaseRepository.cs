using Idyfa.Core.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Idyfa.EntityFrameworkCore;

/// <inheritdoc />
public class IdyfaBaseRepository<TEntity, TKey> : IIdyfaBaseRepository<TEntity, TKey> where TEntity : class
{
    protected readonly IdyfaDbContext _db;
    protected readonly DbSet<TEntity> _set;

    public IdyfaBaseRepository(IdyfaDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _set = _db.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
        _set.Add(entity);
        _db.SaveChanges();
    }

    public async Task AddAndSaveAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _set.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public void Update(TEntity entity)
    {
        _set.Update(entity);
        _db.SaveChanges();
    }

    public async Task UpdateAndSaveAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _set.Update(entity);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public void Delete(TEntity entity)
    {
        _set.Remove(entity);
        _db.SaveChanges();
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _set.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public TEntity FindById(TKey id)
    {
        return _set.Find(id)!;
    }

    public async Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        return (await _set.FindAsync(id, cancellationToken).ConfigureAwait(false))!;
    }

    public IReadOnlyCollection<TEntity> GetAll()
    {
        return _set.ToList();
    }

    public async Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _set.ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}