﻿namespace libs;

public class Collision
{
    private static Collision? _instance;
    private GameEngine engine;
    public static Collision Instance {
        get{
            if(_instance == null)
            {
                _instance = new Collision();
            }
            return _instance;
        }
    }

    private Collision() {
        //INIT PROPS HERE IF NEEDED
        engine = GameEngine.Instance;
    }

    public bool canMove(int x, int y, int dx, int dy)
    {
        int nextX = x + dx;
        int nextY = y + dy;
        
        Map map = engine.GetMap();
        GameObject firstObj = map.Get(nextY, nextX);
        
        
        if ((int)firstObj.Type == 1 || (int)firstObj.Type == 6 || (int)firstObj.Type == 4 || (int)firstObj.Type == 5) // if next object is obstacle, Player cant move
        {
            return false;
        } else if
            ((int)firstObj.Type == 2) // if next object is box, check for next object is obstacle or box. else move the first object
        {
            GameObject secondObj = map.Get(nextY + dy, nextX + dx);
            if ((int)secondObj.Type == 1 || (int)secondObj.Type == 2 || (int)secondObj.Type == 6 || (int)secondObj.Type == 4 || (int)secondObj.Type == 5)
            {
                return false;
            }
            
            else
            {
                firstObj.Move(dx, dy);
                return true;
            }

        } else
        {
            return true;
        }
        
    }
}