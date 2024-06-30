using System.Windows.Input;

namespace libs;

public sealed class InputHandler{

    private static InputHandler? _instance;
    private GameEngine engine;

    public static InputHandler Instance {
        get{
            if(_instance == null)
            {
                _instance = new InputHandler();
            }
            return _instance;
        }
    }

    private InputHandler() {
        //INIT PROPS HERE IF NEEDED
        engine = GameEngine.Instance;
    }

    public void Handle(ConsoleKeyInfo keyInfo)
    {
        GameObject focusedObject = Player.Instance;

        if (focusedObject != null) {
            // Handle keyboard input to move the player
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    
                    if(engine.GameState != GameEngineState.Playing){
                        return;
                    }

                    focusedObject.Move(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    if(engine.GameState != GameEngineState.Playing){
                        return;
                    }

                    focusedObject.Move(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                if(engine.GameState != GameEngineState.Playing){
                        return;
                    }
                    focusedObject.Move(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                if(engine.GameState != GameEngineState.Playing){
                        return;
                    }
                    focusedObject.Move(1, 0);
                    break;
                case ConsoleKey.Z:
                if(engine.GameState != GameEngineState.Playing){
                        return;
                    }
                    if ((keyInfo.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control) {
                        engine.UndoMove();
                    }
                    break;
                case ConsoleKey.Y:
                if(engine.GameState != GameEngineState.Playing){
                        return;
                    }
                    if ((keyInfo.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control) {
                        engine.RedoMove();
                    }
                    break;
                case ConsoleKey.S:

                    if(engine.GameState == GameEngineState.StartScreen){
                        engine.StartTutorial();
                    }

                    if ((keyInfo.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control) {
                        engine.SaveGameToJson();
                    }
                    break;
                case ConsoleKey.L:
                    
                    if(engine.GameState == GameEngineState.StartScreen && engine.IsSavedGameAvailable()){
                        engine.LoadGameFromJson();
                    }
                    break;
                case ConsoleKey.X:
                    if(engine.GameState == GameEngineState.StartScreen){
                        Console.WriteLine("BYE BYE");
                        System.Environment.Exit(1);
                    }
                    if ((keyInfo.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control) {
                        Console.WriteLine("BYE BYE");
                        System.Environment.Exit(1);
                    }
                    break;
                case ConsoleKey.R:
                if(engine.GameState != GameEngineState.Playing){
                        return;
                    }
                    if ((keyInfo.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control && engine.IsSavedGameAvailable()) {
                        engine.LoadGameFromJson();
                    }
                    break;
                    case ConsoleKey.Spacebar:
                if(engine.GameState != GameEngineState.Tutorial){
                        return;
                    }
                    engine.ProceedDialogue();
                    break;
                default:
                    break;
            }
        } 
    }
}