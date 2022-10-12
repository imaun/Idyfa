using Idyfa.Core.Contracts;
using Idyfa.Core.Exceptions;
using Idyfa.Core.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core.Services;

/// <summary>
/// Manages <see cref="User"/>s <see cref="UserUsedPassword"/>s.
/// </summary>
public class IdyfaUserUsedPasswordManager : IIdyfaUserUsedPasswordManager
{
    private readonly IIdyfaUserUsedPasswordRepository _repository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IdyfaOptions _options;
    private readonly IdyfaPasswordOptions _passwordOptions;

    public IdyfaUserUsedPasswordManager(
        IIdyfaUserUsedPasswordRepository repository,
        IPasswordHasher<User> passwordHasher,
        IdyfaOptions options)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _options = options ?? throw new IdyfaOptionsNotFoundException();
        _passwordOptions = options.PasswordOptions;
    }
    
    public async Task<bool> IsPasswordUsedBeforeAsync(User user, string password)
    {
        user.CheckArgumentIsNull(nameof(user));

        if (!_passwordOptions.PreviouslyUsedPasswordsNotAllowed)
            return false;
        
        var hashedPassword = _passwordHasher.HashPassword(user, password);
        var result = await _repository.IsPasswordExistedAsync(user.Id, hashedPassword);
        return result;
    }

    public async Task AddToUsedPasswordsAsync(User user, UserUsedPassword password)
    {
        user.CheckArgumentIsNull(nameof(user));
        password.CheckArgumentIsNull(nameof(password));
        
        await _repository.AddAsync(password);
    }

    public async Task<bool> IsUserPasswordTooOldAsync(User user)
    {
        user.CheckArgumentIsNull(nameof(user));
        var last = await GetLastUserChangePasswordDateAsync(user);
        if (last is null)
            return false;

        return last.Value.AddDays(_passwordOptions.ChangePasswordReminderDays) >= DateTime.UtcNow;
    }

    public async Task<DateTime?> GetLastUserChangePasswordDateAsync(User user)
    {
        user.CheckArgumentIsNull(nameof(user));

        var last = await _repository.GetLastByUserIdAsync(user.Id);
        return last?.CreateDate;
    }
}