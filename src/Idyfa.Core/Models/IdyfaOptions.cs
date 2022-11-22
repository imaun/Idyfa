using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core;

public class IdyfaConfigRoot
{
    
    public IdyfaOptions Idyfa { get; set; }
}



public class IdyfaOptions : IdentityOptions
{
    public IdyfaDbConfig IdyfaDbConfig { get; set; }
    
    public IdyfaUserOptions UserOptions { get; set; }
    
    public IdyfaPasswordOptions PasswordOptions { get; set; }
    
    public IdyfaUserRegistrationOptions Registration { get; set; }
    
    public string TableNamesPrefix { get; set; }
    
    public IdyfaAuthOptions Authentication { get; set; }
}


public class IdyfaUserOptions : UserOptions
{
    public UserNameType UserNameType { get; set; }
    
    public ISet<string> BannedEmails { get; set; }
    
    public ISet<string> BannedUserNames { get; set; }
}

public class IdyfaUserRegistrationOptions
{
    public bool PasswordIsRequired { get; set; }
    
    public bool UserNameIsRequired { get; set; }
    
    public int? UserNameMinLength { get; set; }
    
    public bool EmailIsRequired { get; set; }
    
    public bool PhoneNumberIsRequired { get; set; }
    
    public bool FirstNameIsRequired { get; set; }
    
    public bool LastNameIsRequired { get; set; }
    
    public bool NationalCodeIsRequired { get; set; }
    
}

public class IdyfaPasswordOptions : PasswordOptions
{
    public ISet<string> BannedPasswords { get; set; }

    public bool CanIncludeUserName { get; set; }

    public int? MaxLength { get; set; }

    public string InvalidCharacters { get; set; }

    public bool PreviouslyUsedPasswordsNotAllowed { get; set; }

    public int ChangePasswordReminderDays { get; set; }

}

public class IdyfaDbConfig
{
    public string DbTypeName { get; set; }
    
    public IEnumerable<IdyfaDbConfigItem> Databases { get; set; } 
}

public class IdyfaDbConfigItem
{
    public string Name { get; set; }
    public string ConnectionString { get; set; }
    public int Timeout { get; set; }
}

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