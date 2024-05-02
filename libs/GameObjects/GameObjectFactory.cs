namespace libs;

public class GameObjectFactory : IGameObjectFactory
{
    public GameObject CreateGameObject(dynamic obj) {

        GameObject newObj = new GameObject();
        int type = obj.Type;

        switch (type)
        {
            case (int) GameObjectType.Player:
                newObj = obj.ToObject<Player>();
                break;
            case (int) GameObjectType.Obstacle:
                newObj = obj.ToObject<Obstacle>();
                break;
            case (int) GameObjectType.Box:
                newObj = obj.ToObject<Box>();
                break;
            case (int) GameObjectType.Door:
                newObj = obj.ToObject<Door>();
                break;
            case (int) GameObjectType.Chest:
                newObj = obj.ToObject<Chest>();
                break;
            case (int) GameObjectType.Button:
                newObj = obj.ToObject<Button>();
                break; 
            //case (int) GameObjectType.Goal:
            //newObj = obj.ToObject<Goal>();
            //break;
        }

        return newObj;
    }
}