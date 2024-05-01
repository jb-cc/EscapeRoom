namespace libs;

public class Button : GameObject
{
    
    public Button()
    {
        this.Type = GameObjectType.Button;
        this.CharRepresentation = '⌻';
        this.Color = ConsoleColor.Magenta;
    }


    public void InteractChest(Chest chest)
    {
        if (chest.IsOpen)
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