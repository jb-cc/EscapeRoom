namespace libs;

public class Door: GameObject
{
    
    public Door() : base()
    {
        Type = GameObjectType.Door;
        CharRepresentation = '#';
        Color = ConsoleColor.DarkGreen;
    }

    private bool _isOpen = false;

    public bool IsOpen
    {
        get { return _isOpen; }
        set
        {
            _isOpen = true;
        }
    }

    public override void Interact()
    {
        if (Player.HasKey())
        {
            this.IsOpen = true;
        }
        else
        {
            Console.WriteLine("You need a key to open this door");
        }
    }
}