namespace libs;

public class GameObject : IGameObject, IMovement
{
    private char _charRepresentation = '#';
    private ConsoleColor _color;
    private int _posX;
    private int _posY;
    private int _prevPosX;
    private int _prevPosY;
    private bool _isCollideable = false;

    public GameObjectType Type { get; set; }

    // Default constructor
    public GameObject()
    {
        _posX = 5;
        _posY = 5;
        _color = ConsoleColor.Gray;
    }

    // Constructor with position
    public GameObject(int posX, int posY)
    {
        _posX = posX;
        _posY = posY;
    }

    // Constructor with position and color
    public GameObject(int posX, int posY, ConsoleColor color)
    {
        _posX = posX;
        _posY = posY;
        _color = color;
    }

    // Deep copy constructor
    public GameObject(GameObject gameObject)
    {
        _posX = gameObject.PosX;
        _posY = gameObject.PosY;
        _color = gameObject.Color;
        _charRepresentation = gameObject.CharRepresentation;
        _isCollideable = gameObject.IsCollideable;
        Type = gameObject.Type;
    }

    // Properties
    public char CharRepresentation
    {
        get { return _charRepresentation; }
        set { _charRepresentation = value; }
    }

    public ConsoleColor Color
    {
        get { return _color; }
        set { _color = value; }
    }

    public int PosX
    {
        get { return _posX; }
        set { _posX = value; }
    }

    public int PosY
    {
        get { return _posY; }
        set { _posY = value; }
    }

    public bool IsCollideable
    {
        get { return _isCollideable; }
        set { _isCollideable = value; }
    }

    public int GetPrevPosY() => _prevPosY;
    public int GetPrevPosX() => _prevPosX;

    // Virtual move method to allow overriding
    public virtual void Move(int dx, int dy)
    {
        if (CheckIfPossible(_posX, _posY, dx, dy))
        {
            var oldNode = GameEngine.Instance.CurrentGameState;
            var newNode = new GameStateNode(GameEngine.Instance.CurrentGameState)
            {
                PreviousNode = GameEngine.Instance.CurrentGameState,
                GameVersion = GameEngine.Instance.GameVersion
            };

            GameEngine.Instance.CurrentGameState.NextNode = newNode;
            GameEngine.Instance.CurrentGameState = newNode;

            if (PushBox(_posX, _posY, dx, dy))
            {
                _prevPosX = _posX;
                _prevPosY = _posY;
                _posX += dx;
                _posY += dy;

                newNode.PlayerXPos = Player.Instance.PosX;
                newNode.PlayerYPos = Player.Instance.PosY;

                GameEngine.Instance.CurrentGameState.CurrentMap.GameFinished();
            }
            else
            {
                GameEngine.Instance.CurrentGameState = oldNode;
            }
        }
    }

    // Checks if the new position is possible
    public bool CheckIfPossible(int currentX, int currentY, int dx, int dy)
    {
        var gameObjects = GameEngine.Instance.GetGameObjects();

        return !gameObjects.Any(obj => obj.IsCollideable && obj.PosX == currentX + dx && obj.PosY == currentY + dy);
    }

    // Checks if there is no box in the new position
    public bool CheckIfNoBox(int currentX, int currentY, int dx, int dy)
    {
        var gameObjects = GameEngine.Instance.GetGameObjects();

        return !gameObjects.Any(obj => obj.Type == GameObjectType.Box && obj.PosX == currentX + dx && obj.PosY == currentY + dy);
    }

    // Pushes the box if possible
    public bool PushBox(int currentX, int currentY, int dx, int dy)
    {
        var gameObjects = GameEngine.Instance.GetGameObjects();

        foreach (var obj in gameObjects)
        {
            if (obj.Type == GameObjectType.Box && obj.PosX == currentX + dx && obj.PosY == currentY + dy)
            {
                if (CheckIfPossible(obj.PosX, obj.PosY, dx, dy) && CheckIfNoBox(obj.PosX, obj.PosY, dx, dy))
                {
                    obj.PosX += dx;
                    obj.PosY += dy;
                    return true;
                }
                return false;
            }
        }
        return true;
    }
}
