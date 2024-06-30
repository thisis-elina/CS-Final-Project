namespace libs;
using Newtonsoft.Json;

public class Map {
    
    // Represents the visual layer of the map.
    [JsonProperty]
    private char[,] RepresentationalLayer;
    
    // Represents the game object layer of the map.
    [JsonProperty]
    private GameObject?[,] GameObjectLayer;

    [JsonProperty]
    private int _mapWidth;
    [JsonProperty]
    private int _mapHeight;

    // Default constructor initializes the map with default size.
    public Map () {
        _mapWidth = 30;
        _mapHeight = 8;
        RepresentationalLayer = new char[_mapHeight, _mapWidth];
        GameObjectLayer = new GameObject[_mapHeight, _mapWidth];
    }

    // Parameterized constructor initializes the map with given width and height.
    public Map (int width, int height) {
        _mapWidth = width;
        _mapHeight = height;
        RepresentationalLayer = new char[_mapHeight, _mapWidth];
        GameObjectLayer = new GameObject[_mapHeight, _mapWidth];
    }
    
    // Copy constructor creates a deep copy of the given map.
    public Map (Map map){
        _mapWidth = map.MapWidth;
        _mapHeight = map.MapHeight;
        RepresentationalLayer = map.DeepCopyRepresentationalLayer();
        GameObjectLayer = map.DeepCopyGameObjectLayer();
    }


    // Initializes the map by filling it with default game objects.
    public void Initialize()
    {
        RepresentationalLayer = new char[_mapHeight, _mapWidth];
        GameObjectLayer = new GameObject[_mapHeight, _mapWidth];


        // Initialize the map with some default values
        for (int i = 0; i < GameObjectLayer.GetLength(0); i++)
        {
            for (int j = 0; j < GameObjectLayer.GetLength(1); j++)
            {
                GameObjectLayer[i, j] = new Floor();
            }
        }
    }

    public int MapWidth
    {
        get { return _mapWidth; } // Getter
        set { _mapWidth = value; Initialize();} // Setter
    }

    public int MapHeight
    {
        get { return _mapHeight; } // Getter
        set { _mapHeight = value; Initialize();} // Setter
    }

    // Method to get a game object at a specific position.
    public GameObject Get(int x, int y){
        return GameObjectLayer[x, y];
    }

    // Method to set a game object on the map.
    public void Set(GameObject gameObject){
        int posY = gameObject.PosY;
        int posX = gameObject.PosX;
        int prevPosY = gameObject.GetPrevPosY();
        int prevPosX = gameObject.GetPrevPosX();
    
        // Clear previous position if it was within bounds.
        if (prevPosX >= 0 && prevPosX < _mapWidth &&
                prevPosY >= 0 && prevPosY < _mapHeight)
        {
            if(GameObjectLayer[prevPosY, prevPosX] is Floor){
            GameObjectLayer[prevPosY, prevPosX] = new Floor();
            }
            
        }
        
        // Set new position if it is within bounds.
        if (posX >= 0 && posX < _mapWidth &&
                posY >= 0 && posY < _mapHeight)
        {
            GameObjectLayer[posY, posX] = gameObject;
            RepresentationalLayer[gameObject.PosY, gameObject.PosX] = gameObject.CharRepresentation;
        }
    }

    // Method to create a deep copy of the game object layer.
    public GameObject?[,] DeepCopyGameObjectLayer(){
        
        GameObject?[,] copiedGameObjectLayer = new GameObject[_mapHeight, _mapWidth];

        for (int i = 0; i < GameObjectLayer.GetLength(0); i++)
        {
            for (int j = 0; j < GameObjectLayer.GetLength(1); j++)
            {
                
            // Create a deep copy of each game object based on its type.
            switch (GameObjectLayer[i, j].Type)
            {
                case GameObjectType.Player:
                    copiedGameObjectLayer[i, j] = Player.Instance;
                    break;
                case GameObjectType.Obstacle:
                    copiedGameObjectLayer[i, j] = new Obstacle(GameObjectLayer[i, j]);
                    break;
                case GameObjectType.Box:
                    copiedGameObjectLayer[i, j] = new Box(GameObjectLayer[i, j]);
                    break;
                case GameObjectType.Goal:
                    copiedGameObjectLayer[i, j] = new Goal(GameObjectLayer[i, j]);
                    break;
                case GameObjectType.Floor:
                     copiedGameObjectLayer[i, j] =new Floor(GameObjectLayer[i, j]);
                    break;
            }
            }
        }

        return copiedGameObjectLayer;
    }

    // Method to create a deep copy of the representational layer.
    public char[,] DeepCopyRepresentationalLayer(){

        char[,] copiedPresentationLayer = new char[_mapHeight, _mapWidth];

        for (int i = 0; i < RepresentationalLayer.GetLength(0); i++)
        {
            for (int j = 0; j < RepresentationalLayer.GetLength(1); j++)
            {
                copiedPresentationLayer[i, j] = RepresentationalLayer[i,j];
            }
        }
        return copiedPresentationLayer;
    }

    // Method to check if the game is finished.
    public bool GameFinished(){
        List<GameObject> _boxes = GameEngine.Instance.GetGameObjects().FindAll(e=>e.Type == GameObjectType.Box);
        List<GameObject> _goals = GameEngine.Instance.GetGameObjects().FindAll(e=>e.Type == GameObjectType.Goal);

        int counter = 0;

        // Check if all boxes are on goals.
        foreach(GameObject box in _boxes){
            foreach(GameObject goal in _goals){
                if((box.PosX == goal.PosX) && (box.PosY == goal.PosY)){
                    counter++;
                }
            }
        }
        // If all goals have boxes, the game is finished.
        if(counter == _goals.Count){
            return true;
        }
        
        return false;     
    }

    // Method to update player instances in the map to the singleton instance.
    public void UpdatePlayerInstancesToSingleton()
        {
            for (int y = 0; y < _mapHeight; y++)
            {
                for (int x = 0; x < _mapWidth; x++)
                {
                    if (GameObjectLayer[y, x] != null && GameObjectLayer[y, x].Type == GameObjectType.Player)
                    {
                        GameObjectLayer[y, x] = Player.Instance;
                    }
                }
            }
        }
}