namespace libs;

public class Obstacle : GameObject {
    // Parameterless constructor that initializes a new Obstacle object
    public Obstacle () : base() {
        this.Type = GameObjectType.Obstacle;
        this.CharRepresentation = '█';
        this.Color = ConsoleColor.Cyan;
        IsCollideable = true;
    }

    // Constructor that initializes a Obstacle object from an existing GameObject
    public Obstacle(GameObject gameObject): base(gameObject){
        this.Type = GameObjectType.Obstacle;
        this.CharRepresentation = '█';
        IsCollideable = true;
    }
}