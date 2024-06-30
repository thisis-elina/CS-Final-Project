// Represents a saved map, including the map itself, game objects, and player starting positions
public class SavedMap
{
    private Map _map;
    private List<GameObject> _gameObjects;
    private int _playerStartingX;
    private int _playerStartingY;

    // Default constructor initializes a new map and an empty list of game objects
    public SavedMap()
    {
        _map = new Map();
        _gameObjects = new List<GameObject>();
    }

    // Property to get or set the current map
    public Map CurrentMap
    {
        get { return _map; }
        set { _map = value; }
    }

    // Property to get or set the list of game objects
    public List<GameObject> GameObjects
    {
        get { return _gameObjects; }
        set { _gameObjects = value; }
    }

    // Property to get or set the player's starting X position
    public int PlayerStartingX
    {
        get { return _playerStartingX; }
        set { _playerStartingX = value; }
    }

    // Property to get or set the player's starting Y position
    public int PlayerStartingY
    {
        get { return _playerStartingY; }
        set { _playerStartingY = value; }
    }
}