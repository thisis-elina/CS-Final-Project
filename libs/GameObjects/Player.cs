namespace libs;

public sealed class Player : GameObject
{
    private Direction direction;
    public static Player instance;
    private static readonly object lockObject = new object();

    private Player() : base()
    {
        Type = GameObjectType.Player;
        CharRepresentation = '^';
        Color = ConsoleColor.DarkYellow;
        direction = Direction.North;
    }

    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObject)
                {

                    if (instance == null)
                    {
                        instance = new Player();
                    }
                }
            }
            return instance;
        }
    }

    public override void Move(int dx, int dy){
        changePlayerSprite(dx, dy);
        base.Move(dx, dy);
    }

    public void changePlayerSprite(int dx, int dy){
        int[] move = [dx,dy];
        switch(move){
            case [0,1]:
                this.CharRepresentation = 'v';
                direction = Direction.South;
                break;
            case [1,0]:
                this.CharRepresentation = '>';
                direction = Direction.East;
                break;
            case [0,-1]:
                this.CharRepresentation = '^';
                direction = Direction.North;
                break;
            case [-1,0]:
                this.CharRepresentation = '<';
                direction = Direction.West;
                break;
            default:
                this.CharRepresentation = '^';
                direction = Direction.North;
                break;
        }
    }

    public void Interact(){
       //for now
    }

}