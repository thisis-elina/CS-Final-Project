using System.Windows.Input;

namespace libs;

// Singleton class responsible for handling user input
public sealed class InputHandler
{
    private static InputHandler? _instance; // Singleton instance
    private GameEngine engine; 

    // Public property to access the singleton instance
    public static InputHandler Instance
    {
        get
        {
            // Initialize the instance if not already done
            if (_instance == null)
            {
                _instance = new InputHandler(); 
            }
            return _instance;
        }
    }

    // Private constructor to prevent direct instantiation
    private InputHandler()
    {
        // Initialize properties if needed
        engine = GameEngine.Instance;
    }

    // Method to handle console key inputs
    public void Handle(ConsoleKeyInfo keyInfo)
    {
        GameObject focusedObject = Player.Instance;

        if (focusedObject != null)
        {
            // Handle keyboard input to move the player or perform other actions
            switch (keyInfo.Key)
            {
                 case ConsoleKey.UpArrow:
                    // Move the player up if the game is in the playing state
                    if (engine.GameState != GameEngineState.Playing)
                    {
                        return;
                    }
                    focusedObject.Move(0, -1);
                    break;

                case ConsoleKey.DownArrow:
                    // Move the player down if the game is in the playing state
                    if (engine.GameState != GameEngineState.Playing)
                    {
                        return;
                    }
                    focusedObject.Move(0, 1);
                    break;

                case ConsoleKey.LeftArrow:
                    // Move the player left if the game is in the playing state
                    if (engine.GameState != GameEngineState.Playing)
                    {
                        return;
                    }
                    focusedObject.Move(-1, 0);
                    break;

                case ConsoleKey.RightArrow:
                    // Move the player right if the game is in the playing state
                    if (engine.GameState != GameEngineState.Playing)
                    {
                        return;
                    }
                    focusedObject.Move(1, 0);
                    break;

                case ConsoleKey.Z:
                    // Undo the last move if the game is in the playing state and Control key is pressed
                    if (engine.GameState != GameEngineState.Playing)
                    {
                        return;
                    }
                    if ((keyInfo.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control)
                    {
                        engine.UndoMove();
                    }
                    break;

                case ConsoleKey.Y:
                    // Redo the last undone move if the game is in the playing state and Control key is pressed
                    if (engine.GameState != GameEngineState.Playing)
                    {
                        return;
                    }
                    if ((keyInfo.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control)
                    {
                        engine.RedoMove();
                    }
                    break;

                case ConsoleKey.S:
                    // Start the tutorial if on the start screen or save the game if Control key is pressed
                    if (engine.GameState == GameEngineState.StartScreen)
                    {
                        engine.StartTutorial();
                    }
                    if ((keyInfo.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control)
                    {
                        engine.SaveGameToJson();
                    }
                    break;

                case ConsoleKey.L:
                    // Load the saved game if on the start screen and a saved game is available
                    if (engine.GameState == GameEngineState.StartScreen && engine.IsSavedGameAvailable())
                    {
                        engine.LoadGameFromJson();
                    }
                    break;

                case ConsoleKey.X:
                    // Exit the game if on the start screen or if Control key is pressed
                    if (engine.GameState == GameEngineState.StartScreen)
                    {
                        Console.WriteLine("BYE BYE");
                        System.Environment.Exit(1);
                    }
                    if ((keyInfo.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control)
                    {
                        Console.WriteLine("BYE BYE");
                        System.Environment.Exit(1);
                    }
                    break;

                case ConsoleKey.R:
                    // Reload the game if in the playing state, Control key is pressed, and a saved game is available
                    if (engine.GameState != GameEngineState.Playing)
                    {
                        return;
                    }
                    if ((keyInfo.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control && engine.IsSavedGameAvailable())
                    {
                        engine.LoadGameFromJson();
                    }
                    break;

                case ConsoleKey.Spacebar:
                    // Proceed with the dialogue if in the tutorial state
                    if (engine.GameState != GameEngineState.Tutorial)
                    {
                        return;
                    }
                    engine.ProceedDialogue();
                    break;

                default:
                    // Do nothing for other keys
                    break;
            }
        }
    }
}