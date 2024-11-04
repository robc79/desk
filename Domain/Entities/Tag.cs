namespace Desk.Domain.Entities;

public class Tag
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
                throw new ArgumentException("Name must be supplied.");
            }

            _name = value;
        }
    }

    public DateTimeOffset CreatedOn { get; protected set; }

    public DateTimeOffset? UpdatedOn { get; protected set; }
    
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    
    protected Tag() { }

    public Tag(User owner, string name)
    {
        Owner = owner;
        Name = name;
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}