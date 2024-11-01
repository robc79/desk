namespace Desk.Domain.Entities;

public class TextComment
{
    public int Id { get; protected set; }

    private string _comment;

    public string Comment
    {
        get { return _comment; }

        set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Comment must be supplied.", nameof(value));
            }

            _comment = value;
        }
    }

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

    public int ItemId { get; set; }

    private Item _item;

    public Item Item
    {
        get { return _item; }

        set
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value), "Item must be supplied.");
            }

            _item = value;
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    protected TextComment()
    {
    }

    public TextComment(User owner, Item item, string comment)
    {
        Owner = owner;
        Item = item;
        Comment = comment;
    }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}