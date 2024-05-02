namespace libs;

public class Chest : GameObject
{
    public Chest() : base() {
        Type = GameObjectType.Chest;
        CharRepresentation = '\u2612';
        Color = ConsoleColor.DarkYellow;
    }
    private bool _isOpen = false;
    private int _chestID = 0;
    
    public int ChestID
    {
        get { return _chestID; }
        set
        {
            _chestID = value;
        }
    }
    
    public bool IsOpen
    {
        get { return _isOpen; }
        set
        {
            _isOpen = value;
        }
    }
    
    public override void Interact()
    {
        if (IsOpen)
        {
            Player.Instance.HasKey = true;
            Console.WriteLine("You found a key!");
            return;
        } else {
            Console.WriteLine("Hmpf... seems to be stuck...");
        }
    }
}