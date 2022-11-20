using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core;

public class Role : IdentityRole<string>
{

    protected Role(): base() { }
    
    #region props

    public string Title { get; protected set; }
    
    public string? AltTitle { get; protected set; }
    
    public IEnumerable<RoleClaim> Claims { get; protected set; }
    
    public RoleStatus Status { get; protected set; }
    
    public DateTime CreateDate { get; protected set; }
    
    public DateTime? ModifyDate { get; protected set; }
    
    public DateTime? LastStatusChanged { get; protected set; }

    #endregion

    #region Builder

    public static Role New() =>
        new Role
        {
            Status = RoleStatus.Enabled,
            CreateDate = DateTime.UtcNow
        };

    public Role WithName(string name)
    {
        Name = name;
        return this;
    }

    public Role WithTitle(string title)
    {
        Title = title;
        return this;
    }

    public Role WithAltTitle(string altTitle)
    {
        AltTitle = altTitle;
        return this;
    }

    public Role WithClaims(IEnumerable<RoleClaim> claims)
    {
        if (claims is null || !claims.Any()) return this;

        Claims = claims.ToList();
        return this;
    }

    public void SetStatus(RoleStatus status)
    {
        Status = status;
        LastStatusChanged = DateTime.UtcNow;
    }

    public void MarkAsDeleted() => SetStatus(RoleStatus.Deleted);

    public void Enable() => SetStatus(RoleStatus.Enabled);

    public void Disable() => SetStatus(RoleStatus.Disabled);

    #endregion
}