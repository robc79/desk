namespace Desk.Domain.Entities;

public class Item
{
    public int Id { get; protected set; }

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

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    protected Item()
    {
        Tags = [];
    }

    public Item(User owner, ItemStatus currentStatus, string name) : this()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        Owner = owner;
        CurrentStatus = currentStatus;
        Name = name;
    }
}