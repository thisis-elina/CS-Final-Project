namespace libs;

public class Npc : GameObject {

    public Npc () : base(){
        Type = GameObjectType.Npc;
        CharRepresentation = '☻';
        Color = ConsoleColor.DarkGreen;
        IsCollideable = true;
    }

    public Npc(GameObject gameObject): base(gameObject){
        Type = GameObjectType.Npc;
        CharRepresentation = '☻';
        IsCollideable = true;
    }
}