namespace Idyfa.Core;

public class UserCategory
{
    private UserCategory() { }

    protected UserCategory(
        string title, string description, Guid? parentId = null)
    {
        Title = title;
        Description = description;
        ParentId = parentId;
        
    }
    
    public Guid Id { get; protected set; }
    
    public string Title { get; protected set; }
    
    public string Description { get; protected set; }
    
    public Guid? ParentId { get; protected set; }
    
    public DateTime CreateDate { get; protected set; }
    
    public DateTime? ModifyDate { get; protected set; }
    
    public bool Deleted { get; protected set; }
}