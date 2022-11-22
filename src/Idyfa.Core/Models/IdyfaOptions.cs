using Microsoft.AspNetCore.Identity;
using Idyfa.Core.Services;


namespace Idyfa.Core;

/// <summary>
/// Root class to keep IdyfaOptions nested.
/// </summary>
public class IdyfaConfigRoot
{
    
    public IdyfaOptions Idyfa { get; set; }
}


/// <summary>
/// Main class to keep Idyfa Options.
/// </summary>
public class IdyfaOptions : IdentityOptions
{
    public IdyfaDbConfig IdyfaDbConfig { get; set; }
    public IdyfaUserOptions UserOptions { get; set; }
    public IdyfaPasswordOptions PasswordOptions { get; set; }
    public IdyfaUserRegistrationOptions Registration { get; set; }
    public string TableNamesPrefix { get; set; }
    public IdyfaAuthOptions Authentication { get; set; }
}


/// <summary>
/// Options related to the <see cref="User"/>
/// </summary>
public class IdyfaUserOptions : UserOptions
{
    public UserNameType UserNameType { get; set; }
    public ISet<string> BannedEmails { get; set; }
    public ISet<string> BannedUserNames { get; set; }
}

/// <summary>
/// Options to set the ruled for User Registration process
/// in <see cref="AuthenticationManager"/>
/// </summary>
public class IdyfaUserRegistrationOptions
{
    public bool PasswordIsRequired { get; set; }
    public bool UserNameIsRequired { get; set; }
    public int? UserNameMinLength { get; set; }
    public int? UserNameMaxLength { get; set; }
    public bool EmailIsRequired { get; set; }
    public bool PhoneNumberIsRequired { get; set; }
    public bool FirstNameIsRequired { get; set; }
    public bool LastNameIsRequired { get; set; }
    public bool NationalCodeIsRequired { get; set; }
}


/// <summary>
/// Options used in <see cref="IdyfaPasswordValidator"/>
/// </summary>
public class IdyfaPasswordOptions : PasswordOptions
{
    public IdyfaPasswordOptions()
    {
        BannedPasswords = new HashSet<string>();
    }
    
    public ISet<string> BannedPasswords { get; set; }
    public bool CanIncludeUserName { get; set; }
    public int? MaxLength { get; set; }
    public string InvalidCharacters { get; set; }
    public bool PreviouslyUsedPasswordsNotAllowed { get; set; }
    public int ChangePasswordReminderDays { get; set; }
}


/// <summary>
/// Options for connecting to different Database providers for storing & querying
/// Identity related models.
/// </summary>
public class IdyfaDbConfig
{
    public string DbTypeName { get; set; }
    
    public IEnumerable<IdyfaDbConfigItem> Databases { get; set; } 
}

/// <summary>
/// Database config item for a Database provider.
/// </summary>
public class IdyfaDbConfigItem
{
    public string Name { get; set; }
    public string ConnectionString { get; set; }
    public int Timeout { get; set; }
}


/// <summary>
/// Options related to Authenticating the <see cref="User"/>.
/// </summary>
public class IdyfaAuthOptions
{
    public string CookieName { get; set; }
    public string LoginPath { get; set; }
    public string LogoutPath { get; set; }
    public string AccessDeniedPath { get; set; }
    public TimeSpan ExpireTimeSpan { get; set; }
    public bool SlidingExpiration { get; set; }
    public bool LockoutOnFailure { get; set; }
}