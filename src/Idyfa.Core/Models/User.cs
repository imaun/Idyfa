using Idyfa.Core.enums;
using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core;

public class User : IdentityUser<string>
{
    protected User()
    {
        var p = PermissionRecord.New()
            .WithTitle("")
            .WithCategory("")
            .WithSystemName("");
    }
    
    
    #region props

    public string FirstName { get; protected set; }
    public string LastName { get; protected set; }
    public string NationalCode { get; protected set; }
    public string DisplayName { get; set; }
    public UserStatus Status { get; protected set; }
    public DateTime CreateDate { get; protected set; }
    public DateTime RegisterDate { get; protected set; }
    public DateTime? ModifyDate { get; protected set; }
    public DateTime? LastStatusChanged { get; protected set; }
    
    #endregion
}