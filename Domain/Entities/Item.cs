namespace Desk.Domain.Entities;

public class Item
{
    public int Id { get; protected set; }

    public Guid OwnerId { get; set; }
    
    private User _owner;

    public User Owner
    {
        get { return _owner; }
        
        set
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value), "Owner must be supplied.");
            }

            _owner = value;
        }
    }

    private string _name;

    public string Name
    {
        get { return _name; }

        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Name must be supplied.", nameof(value));
            }

            _name = value;
        }
    }

    private string _description;

    public string Description
    {
        get { return _description; }

        set
        {
            _description = value ?? throw new ArgumentNullException(nameof(value), "Description must be supplied.");
        }
    }

    public ItemStatus CurrentStatus { get; set; }

    public ItemLocation Location { get; set; }

    public ICollection<Tag> Tags { get; protected set; }

    public ICollection<TextComment> TextComments { get; protected set; }

    public DateTimeOffset CreatedOn { get; protected set; }

    public DateTimeOffset? UpdatedOn { get; protected set; }

    public string? ImageName { get; set; }

    public bool IsDeleted { get; set; }
    
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    
    protected Item()
    {
        Tags = [];
        TextComments = [];
    }

    public Item(User owner, ItemStatus currentStatus, string name) : this()
    {
        Owner = owner;
        CurrentStatus = currentStatus;
        Name = name;
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}