namespace libs;

public class Floor : GameObject {
    // Parameterless constructor that initializes a new Floor object
    public Floor () : base()
    {
        Type = GameObjectType.Floor;
        CharRepresentation = '.';
    }

    // Constructor that initializes a Floor object from an existing GameObject
    public Floor(GameObject gameObject): base(gameObject)
    {
        Type = GameObjectType.Floor;
        CharRepresentation = '.';
    }
}