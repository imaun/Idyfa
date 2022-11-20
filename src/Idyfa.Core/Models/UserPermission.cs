namespace Idyfa.Core;


public class UserPermission
{

    protected UserPermission()
    {
    }


    #region props

    public string UserId { get; protected set; }
    
    public Guid PermissionId { get; protected set; }
    
    public DateTime CreateDate { get; protected set; }

    #endregion


    #region Builders

    public static UserPermission New(string userId, Guid permissionId)
    {
        return new UserPermission
        {
            UserId = userId,
            PermissionId = permissionId,
            CreateDate = DateTime.UtcNow
        };
    }

    #endregion
}