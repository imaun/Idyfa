using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core;

public class User : IdentityUser<string>
{
    public User()
    {
    }
    
    #region props

    public string FirstName { get; protected set; }
    
    public string LastName { get; protected set; }
    
    public string NationalCode { get; protected set; }
    
    public string DisplayName { get; set; }
    
    public UserStatus Status { get; protected set; }
    
    public DateTime? BirthDate { get; protected set; }
    
    public DateTime CreateDate { get; protected set; }
    
    public DateTime RegisterDate { get; protected set; }
    
    public DateTime? ModifyDate { get; protected set; }
    
    public DateTime? LastStatusChanged { get; protected set; }
    
    public DateTime? LastVisitDate { get; protected set; }
    
    /// <summary>
    /// ApiKey for authenticating the user via a random string.
    /// </summary>
    public string ApiKey { get; protected set; }
    
    /// <summary>
    /// The code that <see cref="User"/>s can use for referring their friends. 
    /// </summary>
    public string ReferralCode { get; protected set; }
    
    public Guid? CategoryId { get; protected set; }
    #endregion
}