namespace Idyfa.Core;

public class PermissionRecord
{
    protected PermissionRecord()
    {
    }

    public static PermissionRecord New()
    {
        return new PermissionRecord();
    }
    
    public PermissionRecord WithTitle(string title)
    {
        Title = title;
        return this;
    }

    public PermissionRecord WithSystemName(string systemName)
    {
        SystemName = systemName;
        return this;
    }

    public PermissionRecord WithCategory(string category)
    {
        Category = category;
        return this;
    }
    
    public string Title { get; protected set; }
    
    public string SystemName { get; protected set; }
    
    public string Category { get; protected set; }
}