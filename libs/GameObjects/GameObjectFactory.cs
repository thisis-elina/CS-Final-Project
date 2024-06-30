namespace libs;

public class GameObjectFactory : IGameObjectFactory
{
    // Creates a GameObject based on the provided dynamic object
    public GameObject CreateGameObject(dynamic obj)
    {
        // Default GameObject initialization
        GameObject newObj = new GameObject(); 
        int type = obj.Type; 

        // Determine the type of GameObject to create
        switch (type)
        {
            case (int)GameObjectType.Player:
                newObj = Player.Instance; 
                newObj.PosX = obj.PosX;
                newObj.PosY = obj.PosY;
                break;

            case (int)GameObjectType.Obstacle:
                // Convert dynamic object to Obstacle
                newObj = obj.ToObject<Obstacle>();
                break;

            case (int)GameObjectType.Box:
                // Convert dynamic object to Box
                newObj = obj.ToObject<Box>();
                break;

            case (int)GameObjectType.Goal:
                // Convert dynamic object to Goal
                newObj = obj.ToObject<Goal>();
                break;
        }
        // Return the created GameObject
        return newObj;
    }
}