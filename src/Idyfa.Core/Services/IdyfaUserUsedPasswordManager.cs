using Idyfa.Core.Contracts;
using Idyfa.Core.Contracts.Repository;
using Idyfa.Core.Exceptions;
using Idyfa.Core.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core.Services;

public class IdyfaUserUsedPasswordManager : IIdyfaUserUsedPasswordManager
{
    private readonly IIdyfaUserUsedPasswordRepository _repository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IdyfaOptions _options;

    public IdyfaUserUsedPasswordManager(
        IIdyfaUserUsedPasswordRepository repository,
        IPasswordHasher<User> passwordHasher,
        IdyfaOptions options)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _options = options ?? throw new IdyfaOptionsNotFoundException();
    }
    
    public async Task<bool> IsPasswordUsedBeforeAsync(User user, string password)
    {
        user.CheckArgumentIsNull(nameof(user));
        var result = await _repository.IsPasswordExistedAsync(user.Id, password);
        return result;
    }

    public async Task AddToUsedPasswordsAsync(User user, UserUsedPassword password)
    {
        user.CheckArgumentIsNull(nameof(user));

        await _repository.AddAsync(password);
    }

    public async Task<bool> IsUserPasswordTooOldAsync(User user)
    {
        user.CheckArgumentIsNull(nameof(user));
        return true;
    }

    public async Task<DateTime?> GetLastUserChangePasswordDateAsync(User user)
    {
        user.CheckArgumentIsNull(nameof(user));

        var last = await _repository.GetLastByUserIdAsync(user.Id);
        return last?.CreateDate;
    }
}