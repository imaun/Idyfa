namespace Idyfa.Core;

public class PermissionRecord
{
    protected PermissionRecord()
    {
    }

    public static PermissionRecord New()
    {
        var created = new PermissionRecord
        {
            Id = Guid.NewGuid(),
            CreateDate = DateTime.UtcNow
        };
        return created;
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

    public PermissionRecord WithDescription(string description)
    {
        Description = description;
        return this;
    }
    
    public Guid Id { get; protected set; }
    
    public string Title { get; protected set; }
    
    public string SystemName { get; protected set; }
    
    public string Category { get; protected set; }
    
    public string Description { get; protected set; }
    
    public DateTime CreateDate { get; protected set; }
}