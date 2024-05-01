namespace libs;

public class Chest
{
    public Chest() : base() {
        Type = GameObjectType.Chest;
        CharRepresentation = '\u2612';
        Color = ConsoleColor.DarkYellow;
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
        if (Chest.IsOpen)
        {
            Player.HasKey = true;
            Console.WriteLine("You found a key!");
            return;
        } else {
            Console.WriteLine("Hmpf... seems to be stuck...");
        }
    }
}