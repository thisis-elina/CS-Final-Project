namespace libs;

public class Npc : GameObject {
    // Parameterless constructor that initializes a new Npc object
    public Npc () : base()
    {
        Type = GameObjectType.Npc;
        CharRepresentation = '☻';
        Color = ConsoleColor.DarkGreen;
        IsCollideable = true;
    }

    // Constructor that initializes a Npc object from an existing GameObject
    public Npc(GameObject gameObject): base(gameObject){
        Type = GameObjectType.Npc;
        CharRepresentation = '☻';
        IsCollideable = true;
    }
}