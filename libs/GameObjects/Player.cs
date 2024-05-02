namespace libs;

public sealed class Player : GameObject {

    private static Player? _instance;
    private Collision collision;

    private bool _hasKey = false;
    private GameEngine engine;


    public bool HasKey
    {
        get { return _hasKey; }
        set
        {
            _hasKey = value;
        }
    }

    
    public static Player Instance {
        get{
            if(_instance == null)
            {
                _instance = new Player();
            }
            return _instance;
        }
    }
    private Player () : base(){
        Type = GameObjectType.Player;
        CharRepresentation = '☻';
        Color = ConsoleColor.DarkYellow;
        engine = GameEngine.Instance;

    }

    public override void Move(int dx, int dy)
    {
        collision = Collision.Instance;
        
        if (collision.canMove(this.PosX , this.PosY , dx, dy)){
            
        SetPrevPosX(this.PosX);
        SetPrevPosY(this.PosY);
        this.PosX += dx;
        this.PosY += dy;
        }
    }

    public override void Interact()
    {
        Map map = engine.GetMap();
        GameObject leftObj = map.Get(this.PosY, this.PosX -1);
        Console.WriteLine(leftObj);
        GameObject rightObj = map.Get(this.PosY, this.PosX +1);
        
        GameObject upObj = map.Get(this.PosY -1, this.PosX);
        GameObject downObj = map.Get(this.PosY +1, this.PosX);
        
        leftObj.Interact();
        rightObj.Interact();
        upObj.Interact();
        downObj.Interact();
        
    }
}