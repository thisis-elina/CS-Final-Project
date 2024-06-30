namespace libs;

public class Goal : GameObject {
    // Parameterless constructor that initializes a new Goal object
    public Goal () : base()
    {
        Type = GameObjectType.Goal;
        CharRepresentation = '⚑';
        Color = ConsoleColor.Red;
    }

    // Constructor that initializes a Goal object from an existing GameObject
    public Goal(GameObject gameObject): base(gameObject)
    {
        Type = GameObjectType.Goal;
        CharRepresentation = '⚑';
    }
}