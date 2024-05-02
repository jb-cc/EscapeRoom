namespace libs;

public class Button : GameObject
{
    
    public Button()
    {
        this.Type = GameObjectType.Button;
        this.CharRepresentation = '⌻';
        this.Color = ConsoleColor.DarkGreen;
    }

    private GameObject _connectedChest = null;
    
    public GameObject ConnectedChest
    {
        get { return _connectedChest; }
        set
        {
            _connectedChest = value;
        }
    }

    private int _chestID = 0;
    
    public int ChestID
    {
        get { return _chestID; }
        set
        {
            _chestID = value;
        }
    }
    
    
    public void InteractChest(Chest chest)
    {
        if (!chest.IsOpen)
        {
            chest.IsOpen = true;
            Console.WriteLine("Click! There was a wooden sound in the distance");
            return;
        }
        else
        {
            Console.WriteLine("Nothing happens...");
        }
    }
}