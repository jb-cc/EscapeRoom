namespace libs;

public class Door: GameObject
{
    
    public Door() : base()
    {
        Type = GameObjectType.Door;
        CharRepresentation = '#';
        Color = ConsoleColor.DarkGreen;
        engine = GameEngine.Instance;

    }

    private bool _isOpen = false;
    private GameEngine engine;


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
        if (Player.Instance.HasKey)
        {
            this.IsOpen = true;
            engine.SetTextBoxText("Click! You opened the door");
        }
        else
        {
            engine.SetTextBoxText("You need a key to open this door");
        }
    }
}