﻿namespace libs;

public class Button
{
    
    public Button()
    {
        this.Type = GameObjectType.Button;
        this.CharRepresentation = '⌻';
        this.Color = ConsoleColor.Magenta;
    }
    
    
    public override void Interact()
    {
        Chest.IsOpen = true;
        Console.WriteLine("Click! There was a wooden sound in the distance.");
    }
}