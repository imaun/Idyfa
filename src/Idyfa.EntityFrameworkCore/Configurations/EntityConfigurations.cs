using Idyfa.Core;

namespace Idyfa.EntityFrameworkCore.Configurations;

public static partial class EntityConfigurations
{
    private static Dictionary<Type, string> _tableNames = new Dictionary<Type, string>()
    {
        { typeof(User), nameof(User) },
        { typeof(Role), nameof(Role) },
        { typeof(UserCategory), nameof(UserCategory) },
        { typeof(UserClaim), nameof(UserClaim) },
        { typeof(UserLogin), nameof(UserLogin) },
        { typeof(UserRole), nameof(UserRole) },
        { typeof(RoleClaim), nameof(RoleClaim) },
        { typeof(UserLoginRecord), nameof(UserLoginRecord) },
        { typeof(UserToken), nameof(UserToken) },
        { typeof(UserUsedPassword), nameof(UserUsedPassword) },
        { typeof(Permission), nameof(Permission) },
        { typeof(RolePermission), nameof(RolePermission) }
    };

    private static string GetTableName(Type t, string tableNamePrefix = "")
    {
        if (!_tableNames.ContainsKey(t))
        {
            throw new Exception($"The type '{t.FullName}' is not a valid Idyfa Entity type.");
        }

        return $"{tableNamePrefix}{_tableNames[t]}";
    }
}