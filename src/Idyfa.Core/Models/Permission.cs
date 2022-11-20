namespace Idyfa.Core;

public class Permission
{
    protected Permission()
    {
    }

    public static Permission New()
    {
        var created = new Permission
        {
            Id = Guid.NewGuid(),
            CreateDate = DateTime.UtcNow
        };
        return created;
    }
    
    public Permission WithTitle(string title)
    {
        Title = title;
        return this;
    }

    public Permission WithSystemName(string systemName)
    {
        SystemName = systemName;
        return this;
    }

    public Permission WithCategory(string category)
    {
        Category = category;
        return this;
    }

    public Permission WithDescription(string description)
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