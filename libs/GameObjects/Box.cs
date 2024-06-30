namespace libs;

public class Box : GameObject {
    // Parameterless constructor that initializes a new Box object
    public Box() : base()
    {
        Type = GameObjectType.Box;
        CharRepresentation = '○';
        Color = ConsoleColor.DarkGreen;
    }

    // Constructor that initializes a Box object from an existing GameObject
    public Box(GameObject gameObject) : base(gameObject)
    {
        Type = GameObjectType.Box;
        CharRepresentation = '○';
    }
}