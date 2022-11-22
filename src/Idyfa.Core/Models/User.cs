using System.Runtime.Serialization.Json;
using Microsoft.AspNetCore.Identity;
using static Idyfa.Core.Extensions.CoreExts;

namespace Idyfa.Core;

public class User : IdentityUser<string>
{
    protected User()
    {
        Permissions = new List<UserPermission>();
    }


    #region Builders

    public static User New()
    {
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            CreateDate = DateTime.UtcNow,
            Status = UserStatus.Created,
            ApiKey = GenerateToken(20),
            ReferralCode = GenerateReferralCode()
        };
        return user;
    }

    public static User RegisterUser(
        string userName, string email, string displayName,
        string phoneNumber, string firstName, string lastName)
    {
        var user = User.New()
            .WithUserName(userName)
            .WithEmail(email)
            .WithDisplayName(displayName)
            .WithPhoneNumber(phoneNumber)
            .WithFirstName(firstName)
            .WithLastName(lastName)
            .WithPhoneNumber(phoneNumber)
            .WithRegisterDate(DateTime.UtcNow);

        return user;
    }

    public User WhenModified()
    {
        ModifyDate = DateTime.UtcNow;
        return this;
    }

    public User WithId(string id)
    {
        Id = id;
        return this;
    }

    public User WithUserName(string userName)
    {
        UserName = userName;
        return this;
    }

    public User WithPhoneNumber(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
        return this;
    }

    public User WithFirstName(string firstName)
    {
        FirstName = firstName;
        return this;
    }

    public User WithLastName(string lastName)
    {
        LastName = lastName;
        return this;
    }

    public User WithEmail(string email)
    {
        Email = email;
        return this;
    }

    public User WithNationalCode(string nationalCode)
    {
        NationalCode = nationalCode;
        return this;
    }

    public User WithStatus(UserStatus status)
    {
        Status = status;
        LastStatusChanged = DateTime.UtcNow;
        return this;
    }

    public User WithBirthDate(DateTime? birthDate)
    {
        BirthDate = birthDate;
        return this;
    }

    public User WithRegisterDate(DateTime registerDate)
    {
        RegisterDate = registerDate;
        return this;
    }

    public User WithDisplayName(string displayName)
    {
        DisplayName = displayName;
        return this;
    }

    public User WithApiKey(string apiKey)
    {
        ApiKey = apiKey;
        return this;
    }

    public User WithReferralCode(string referralCode)
    {
        ReferralCode = referralCode;
        return this;
    }

    public User WithCategoryId(Guid categoryId)
    {
        CategoryId = categoryId;
        return this;
    }

    public User SetTwoFactorCode(string twoFactorCode)
    {
        LastTwoFactorCode = twoFactorCode;
        LastTwoFactorCodeTime = DateTime.UtcNow;
        return this;
    }

    #endregion
    
    #region props

    public string? FirstName { get; protected set; }
    
    public string? LastName { get; protected set; }
    
    public string? NationalCode { get; protected set; }
    
    public string? DisplayName { get; set; }
    
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
    
    /// <summary>
    /// The Last TwoFactorCode that has been generated for this User.
    /// </summary>
    public string LastTwoFactorCode { get; protected set; }
    
    
    public DateTime? LastTwoFactorCodeTime { get; protected set; }
   
    #endregion


    #region Navigations

    public ICollection<UserPermission> Permissions { get; protected set; }

    #endregion
}