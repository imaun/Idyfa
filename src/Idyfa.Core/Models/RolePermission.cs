namespace Idyfa.Core;

public class RolePermission
{
    protected RolePermission()
    {
    }

    #region props

    public string RoleId { get; protected set; }
    
    public Guid PermissionId { get; protected set; }
    
    public DateTime CreateDate { get; protected set; }

    #endregion

    public static RolePermission New(string roleId, Guid permissionId)
    {
        return new RolePermission
        {
            RoleId = roleId,
            PermissionId = permissionId,
            CreateDate = DateTime.UtcNow
        };
    }
}