namespace Desk.Domain.Entities;

public class User 
{
    public int Id { get; protected set; }

    private string _username;

    public string Username
    {
        get { return _username; }

        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Username must be supplied.");
            }

            _username = value;
        }
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public User(string username)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        Username = username;
    }
}