using Newtonsoft.Json;

namespace libs;


// Represents a node in the game state linked list, which holds the state of the game at a particular point in time.
public class GameStateNode
{
    private double _remainingTime;
    private Map _currentMap;
    private List<GameObject> _currentGameObjects = new List<GameObject>();
    private int _playerXPos;
    private int _playerYPos;
    private GameStateNode? _previousNode;
    private GameStateNode? _nextNode;
    private int _currentMapIndex;
    private int _gameVersion = 0;

    // Default constructor initializes a new map and an empty list of game objects.
    public GameStateNode()
    {
        _currentMap = new Map();
        _currentMapIndex = 0;
        _currentGameObjects = new List<GameObject>();
    }

    // Copy constructor creates a deep copy of the given GameStateNode.
    public GameStateNode(GameStateNode gameState)
    {
        _currentGameObjects = gameState.DeepCopyGameObjects();
        _currentMap = gameState.DeepCopyMap();
        _playerXPos = gameState.PlayerXPos;
        _playerYPos = gameState.PlayerYPos;
        _currentMapIndex = gameState.CurrentMapIndex;
        _remainingTime = gameState.RemainingTime;
    }

    // Properties to access private fields
    public Map CurrentMap
    {
        get { return _currentMap; }
        set { _currentMap = value; }
    }

    public List<GameObject> CurrentGameObjects
    {
        get { return _currentGameObjects; }
        set { _currentGameObjects = value; }
    }

    public int PlayerXPos
    {
        get { return _playerXPos; }
        set { _playerXPos = value; }
    }

    public int PlayerYPos
    {
        get { return _playerYPos; }
        set { _playerYPos = value; }
    }

    public GameStateNode? PreviousNode
    {
        get { return _previousNode; }
        set { _previousNode = value; }
    }

    public GameStateNode? NextNode
    {
        get { return _nextNode; }
        set { _nextNode = value; }
    }

    public int CurrentMapIndex
    {
        get { return _currentMapIndex; }
        set { _currentMapIndex = value; }
    }

    public int GameVersion
    {
        get { return _gameVersion; }
        set { _gameVersion = value; }
    }

    public double RemainingTime
    {
        get { return _remainingTime; }
        set { _remainingTime = value; }
    }

    // Creates a deep copy of the list of game objects
    public List<GameObject> DeepCopyGameObjects()
    {
        List<GameObject> copiedList = new List<GameObject>();

        // Loop through each game object and create a new instance based on its type
        foreach (GameObject gameObject in _currentGameObjects)
        {
            switch (gameObject.Type)
            {
                case GameObjectType.Player:
                    copiedList.Add(Player.Instance);
                    break;
                case GameObjectType.Obstacle:
                    copiedList.Add(new Obstacle(gameObject)); // Create a new Obstacle
                    break;
                case GameObjectType.Box:
                    copiedList.Add(new Box(gameObject)); // Create a new Box
                    break;
                case GameObjectType.Goal:
                    copiedList.Add(new Goal(gameObject)); // Create a new Goal
                    break;
                case GameObjectType.Floor:
                    copiedList.Add(new Floor(gameObject)); // Create a new Floor
                    break;
            }
        }

        return copiedList;
    }

    // Creates a deep copy of the current map
    public Map DeepCopyMap()
    {
        return new Map(_currentMap);
    }

    // Updates player instances in the game objects list to the singleton instance of Player
    public void UpdatePlayerInstancesToSingleton()
    {
        for (int i = 0; i < _currentGameObjects.Count; i++)
        {
            if (_currentGameObjects[i].Type == GameObjectType.Player)
            {
                _currentGameObjects[i] = Player.Instance;
            }
        }
    }
}