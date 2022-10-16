namespace Idyfa.Core;

public class RolePermission
{
    private RolePermission()
    {
    }

    #region props

    public string RoleId { get; protected set; }
    
    public Guid PermissionRecordId { get; protected set; }
    
    public DateTime CreateDate { get; protected set; }

    #endregion

    public static RolePermission New()
    {
        var permission = new RolePermission();
        permission.CreateDate = DateTime.UtcNow;
        return permission;
    }

    public RolePermission WithRoleId(string roleId)
    {
        RoleId = roleId;
        return this;
    }

    public RolePermission WithPermissionRecordId(Guid permissionRecordId)
    {
        PermissionRecordId = permissionRecordId;
        return this;
    }
}