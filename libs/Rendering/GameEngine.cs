﻿using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Nodes;

namespace libs;

using System.Security.Cryptography;
using Newtonsoft.Json;

public sealed class GameEngine
{

    private GameEngineState _gameState;

    public GameEngineState GameState
    {
        get { return _gameState; }
        set { _gameState = value; }
    }

    private static GameEngine? _instance;
    private DialogueBox dialogueBox;

    private IGameObjectFactory gameObjectFactory;

    private int _frameRate = 69;

    private TimeLord _timeLord;

    public static GameEngine Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameEngine();
            }
            return _instance;
        }
    }

    private GameStateNode _currentGameState;


    public GameStateNode CurrentGameState
    {
        get { return _currentGameState; }
        set { _currentGameState = value; }
    }

    public int FrameRate
    {
        get { return _frameRate; }
        set { _frameRate = value; }
    }

    private int _gameVersion = 3;

    public int GameVersion
    {
        get { return _gameVersion; }
        set { _gameVersion = value; }
    }

    private List<SavedMap> _savedMaps;

    private bool _isGameComplete = false;

    public bool IsGameComplete
    {
        get { return _isGameComplete; }
        set { _isGameComplete = value; }
    }

    private GameEngine()
    {
        //INIT PROPS HERE IF NEEDED
        gameObjectFactory = new GameObjectFactory();
        _currentGameState = new GameStateNode();
        _savedMaps = new List<SavedMap>();
        dialogueBox = DialogueBox.Instance;
        _timeLord = TimeLord.Instance;
        _gameState = GameEngineState.StartScreen;
    }

    private GameObject? _focusedObject;

    public Map GetMap()
    {
        return _currentGameState.CurrentMap;
    }

    public List<GameObject> GetGameObjects()
    {
        return _currentGameState.CurrentGameObjects;
    }

    public void Setup()
    {

        Console.OutputEncoding = System.Text.Encoding.UTF8;

        dynamic gameData = FileHandler.ReadJson();

        //gameData.maps

        foreach (var mapData in gameData.maps)
        {
            Map tempMap = new Map();
            List<GameObject> objList = new List<GameObject>();

            tempMap.MapWidth = mapData.width;
            tempMap.MapHeight = mapData.height;

            foreach (var gameObject in mapData.gameObjects)
            {
                AddGameObjectToList(CreateGameObject(gameObject), objList);

            }

            SavedMap savedMap = new SavedMap();
            savedMap.CurrentMap = tempMap;
            savedMap.GameObjects = objList;

            savedMap.PlayerStartingX = savedMap.GameObjects.Find(x => x.Type == GameObjectType.Player).PosX;
            savedMap.PlayerStartingY = savedMap.GameObjects.Find(x => x.Type == GameObjectType.Player).PosY;

            _savedMaps.Add(savedMap);


            _currentGameState.CurrentMap = new Map(_savedMaps[0].CurrentMap);
            _currentGameState.CurrentGameObjects = _savedMaps[0].GameObjects;
            _currentGameState.CurrentMapIndex = 0;
            _currentGameState.GameVersion = _gameVersion;

            _focusedObject = Player.Instance;
            _currentGameState.PlayerXPos = _focusedObject.PosX;
            _currentGameState.PlayerYPos = _focusedObject.PosY;
        }
    }

    public void Render()
    {

        //Clean the map
        Console.Clear();

        switch (_gameState)
        {

            case GameEngineState.StartScreen:
                DisplayStartScreen();
                break;
            case GameEngineState.Tutorial:
                DisplayTutorialScreen();
                break;
            case GameEngineState.Playing:
                DisplayGame();
                break;
            default:
                break;
        }




        if (_currentGameState.CurrentMap.GameFinished())
        {
            LoadNextLevel();
        }
    }

    private void DisplayTutorialScreen(){
        dialogueBox.RenderDialogue();
    }


    public void DisplayStartScreen()
    {
        Console.WriteLine("Ⓢ ⓞ ⓚ ⓞ ⓑ ⓐ ⓝ");
        Console.WriteLine("---------------");
        Console.WriteLine("[S] Start Game ");

        if (IsSavedGameAvailable())
        {
            Console.WriteLine("[L] Load Game");
        }
        Console.WriteLine("[X] Exit Game");
    }

    public bool IsSavedGameAvailable()
    {
        try
        {
            GameStateNode savedGame = FileHandler.LoadGameFromJson();
            return savedGame.GameVersion == _gameVersion;
        }
        catch (FileNotFoundException)
        {
            return false;
        }
        catch (Exception)
        {
            // Handle other exceptions if necessary
            return false;
        }
    }

    public void DisplayGame()
    {

        Console.WriteLine("Time:" + _timeLord.GetTime());
        _currentGameState.RemainingTime = _timeLord.GetTime();
        _currentGameState.CurrentMap.Initialize();

        PlaceGameObjects();

        //Render the map
        for (int i = 0; i < _currentGameState.CurrentMap.MapHeight; i++)
        {
            for (int j = 0; j < _currentGameState.CurrentMap.MapWidth; j++)
            {
                DrawObject(_currentGameState.CurrentMap.Get(i, j));
            }
            Console.WriteLine();
        }
    }




    // Method to create GameObject using the factory from clients
    public GameObject CreateGameObject(dynamic obj)
    {
        return gameObjectFactory.CreateGameObject(obj);
    }

    public void AddGameObject(GameObject gameObject)
    {
        _currentGameState.CurrentGameObjects.Add(gameObject);
    }

    public void AddGameObjectToList(GameObject gameObject, List<GameObject> list)
    {
        list.Add(gameObject);
    }

    private void PlaceGameObjects()
    {

        _currentGameState.CurrentGameObjects.ForEach(delegate (GameObject obj)
        {
            _currentGameState.CurrentMap.Set(obj);
        });
    }

    private void DrawObject(GameObject gameObject)
    {

        Console.ResetColor();

        if (gameObject != null)
        {
            Console.ForegroundColor = gameObject.Color;
            Console.Write(gameObject.CharRepresentation);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(' ');
        }
    }

    public void UndoMove()
    {
        if (_currentGameState.PreviousNode == null)
        {
            return;
        }

        _currentGameState = _currentGameState.PreviousNode;

        TimeLord.Instance.SetTime(_currentGameState.RemainingTime);

        Player.Instance.PosX = _currentGameState.PlayerXPos;
        Player.Instance.PosY = _currentGameState.PlayerYPos;
        _currentGameState.UpdatePlayerInstancesToSingleton();
        _currentGameState.CurrentMap.UpdatePlayerInstancesToSingleton();
        Render();
    }

    public void RedoMove()
    {
        if (_currentGameState.NextNode == null)
        {
            return;
        }
        _currentGameState = _currentGameState.NextNode;

        TimeLord.Instance.SetTime(_currentGameState.RemainingTime);

        Player.Instance.PosX = _currentGameState.PlayerXPos;
        Player.Instance.PosY = _currentGameState.PlayerYPos;
        _currentGameState.UpdatePlayerInstancesToSingleton();
        _currentGameState.CurrentMap.UpdatePlayerInstancesToSingleton();
        Render();

    }

    public void SaveGameToJson()
    {
        FileHandler.SaveGameToJson(_currentGameState);
    }

    public void LoadGameFromJson()
    {
        _gameState = GameEngineState.Playing;
        GameStateNode lastGame = FileHandler.LoadGameFromJson();

        if (lastGame.GameVersion == _gameVersion)
        {
            _currentGameState = lastGame;

            TimeLord.Instance.SetTime(_currentGameState.RemainingTime);

            Player.Instance.PosX = _currentGameState.PlayerXPos;
            Player.Instance.PosY = _currentGameState.PlayerYPos;

            _currentGameState.UpdatePlayerInstancesToSingleton();
            _currentGameState.CurrentMap.UpdatePlayerInstancesToSingleton();

            Render();
        }
    }

    public void LoadNextLevel()
    {
        int currentIndex = _currentGameState.CurrentMapIndex;

        if (currentIndex + 1 < _savedMaps.Count)
        {

            int newIndex = ++currentIndex;

            GameStateNode node = new GameStateNode();

            node.CurrentMap = new Map(_savedMaps[newIndex].CurrentMap);
            node.CurrentGameObjects = _savedMaps[newIndex].GameObjects;
            node.CurrentMapIndex = newIndex;

            _focusedObject = Player.Instance;
            //
            _focusedObject.PosX = _savedMaps[newIndex].PlayerStartingX;
            _focusedObject.PosY = _savedMaps[newIndex].PlayerStartingX;


            node.PlayerXPos = _focusedObject.PosX;
            node.PlayerYPos = _focusedObject.PosY;
            _currentGameState = node;
            Render();
        }
        else
        {
            _isGameComplete = true;
            _gameState = GameEngineState.Finished;
        }
    }

    public void StartTutorial(){
        _gameState = GameEngineState.Tutorial;
    }

    public void StartGame(){
        _gameState = GameEngineState.Playing;
    }

    public void ProceedDialogue(){
        
        if(!dialogueBox.ProceedDialogue()){
            _gameState = GameEngineState.Playing;
        }
    }
}