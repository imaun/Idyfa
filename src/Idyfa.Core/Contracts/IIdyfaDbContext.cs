namespace Idyfa.Core.Contracts;

public interface IIdyfaDbContext
{
    bool EnsureCreated();

    Task<bool> EnsureCreatedAsync(CancellationToken cancellationToken = default);

    void MigrateDb();

    Task MigrateDbAsync(CancellationToken cancellationToken = default);

    bool CanConnect();

    Task<bool> CanConnectAsync(CancellationToken cancellationToken = default);
}