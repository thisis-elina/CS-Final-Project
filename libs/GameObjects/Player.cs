namespace libs;

// Sealed Player class inheriting from GameObject to prevent further inheritance
public sealed class Player : GameObject
{
    private Direction direction; 
    private static Player instance; 
    private static readonly object lockObject = new object(); // Lock object for thread safety

    // Private constructor to prevent direct instantiation
    private Player() : base()
    {
        Type = GameObjectType.Player; 
        CharRepresentation = '^';
        Color = ConsoleColor.DarkYellow;
        direction = Direction.North;
    }

    // Public property to access the singleton instance
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObject) // Ensure thread safety
                {
                    if (instance == null)
                    {
                        instance = new Player(); // Instantiate the singleton instance
                    }
                }
            }
            return instance;
        }
    }

    // Override the Move method to change the player sprite based on movement
    public override void Move(int dx, int dy)
    {
        ChangePlayerSprite(dx, dy);
        base.Move(dx, dy);
    }

    // Change the player sprite and direction based on movement
    public void ChangePlayerSprite(int dx, int dy)
    {
        int[] move = new int[] { dx, dy };
        switch (move)
        {
            case int[] when move.SequenceEqual(new int[] { 0, 1 }):
                CharRepresentation = 'v'; 
                direction = Direction.South;
                break;
            case int[] when move.SequenceEqual(new int[] { 1, 0 }):
                CharRepresentation = '>';
                direction = Direction.East;
                break;
            case int[] when move.SequenceEqual(new int[] { 0, -1 }):
                CharRepresentation = '^';
                direction = Direction.North;
                break;
            case int[] when move.SequenceEqual(new int[] { -1, 0 }):
                CharRepresentation = '<';
                direction = Direction.West;
                break;
            default:
                CharRepresentation = '^';
                direction = Direction.North;
                break;
        }
    }

    // Placeholder method for player interactions
    public void Interact()
    {
        // Interaction logic to be implemented
    }
}