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

    public static RolePermission New()
    {
        return new RolePermission
        {
            CreateDate = DateTime.UtcNow
        };
    }

    public RolePermission WithRoleId(string roleId)
    {
        RoleId = roleId;
        return this;
    }

    public RolePermission WithPermissionId(Guid permissionId)
    {
        PermissionId = permissionId;
        return this;
    }
}