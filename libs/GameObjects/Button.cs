namespace libs;

public class Button : GameObject
{
    
    public Button()
    {
        this.Type = GameObjectType.Button;
        this.CharRepresentation = '⌻';
        this.Color = ConsoleColor.DarkGreen;
        engine = GameEngine.Instance;

    }

    private Chest _connectedChest = null;
    
    public Chest ConnectedChest
    {
        get { return _connectedChest; }
        set
        {
            _connectedChest = value;
        }
    }

    private int _chestID = 0;
    private GameEngine engine;

    
    public int ChestID
    {
        get { return _chestID; }
        set
        {
            _chestID = value;
        }
    }
    
    
    public override void Interact()
    {
        if (!ConnectedChest.IsOpen)
        {
            ConnectedChest.IsOpen = true;
            engine.SetTextBoxText("Click! There was a wooden sound in the distance");
            return;
        }
        else
        {
            engine.SetTextBoxText("Nothing happens...");
        }
    }
}