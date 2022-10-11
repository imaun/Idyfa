using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core;

public class IdyfaOptions : IdentityOptions
{
    
    public UserNameType UserNameType { get; set; }
    
    public IdyfaUserOptions UserOptions { get; set; }
    
    public IdyfaPasswordOptions PasswordOptions { get; set; }
    
    public IdyfaUserRegistrationOptions Registration { get; set; }
}


public class IdyfaUserOptions
{
    public ISet<string> BannedEmails { get; set; }
    
    public ISet<string> BannedUserNames { get; set; }
}

public class IdyfaUserRegistrationOptions
{
    public bool PasswordIsRequired { get; set; }
    
    public bool UserNameIsRequired { get; set; }
    
    public int? UserNameMinLength { get; set; }
    
    public bool EmailIsRequired { get; set; }
    
    public bool FirstNameIsRequired { get; set; }
    
    public bool LastNameIsRequired { get; set; }
    
    public bool NationalCodeIsRequired { get; set; }
    
}

public class IdyfaPasswordOptions : PasswordOptions
{
    public ISet<string> BannedPasswords { get; set; }
    
    public bool CanIncludeUserName { get; set; }
    
    public int? MaxLength { get; set; }
}