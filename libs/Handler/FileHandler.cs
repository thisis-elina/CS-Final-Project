using System.Reflection.Metadata.Ecma335;
using Newtonsoft.Json;

namespace libs;

using Newtonsoft.Json;

public static class FileHandler
{
    private static string filePath; 
    private readonly static string envVar = "GAME_SETUP_PATH"; 

    // Static constructor to initialize the file path
    static FileHandler()
    {
        Initialize();
    }

    // Initializes the file path from the environment variable
    private static void Initialize()
    {
        if (Environment.GetEnvironmentVariable(envVar) != null)
        {
            filePath = Environment.GetEnvironmentVariable(envVar);
        }
    }

    // Reads JSON content from the file and returns the deserialized dynamic object
    public static dynamic ReadJson()
    {
        if (string.IsNullOrEmpty(filePath))
        {
            throw new InvalidOperationException("JSON file path not provided in environment variable");
        }

        try
        {
            string jsonContent = File.ReadAllText(filePath); // Read the file content
            dynamic jsonData = JsonConvert.DeserializeObject(jsonContent); // Deserialize the JSON content
            return jsonData;
        }
        catch (FileNotFoundException)
        {
            throw new FileNotFoundException($"JSON file not found at path: {filePath}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error reading JSON file: {ex.Message}");
        }
    }

    // Saves the current game state to a JSON file
    public static void SaveGameToJson(GameStateNode _currentSate)
    {
        var settings = new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects
        };
        string jsonString = JsonConvert.SerializeObject(_currentSate, settings); // Serialize the game state
        File.WriteAllText($@"SavedGame.json", jsonString); // Write the JSON string to a file
    }

    // Loads the game state from a JSON file
    public static GameStateNode LoadGameFromJson()
    {
        try
        {
            string jsonContent = File.ReadAllText($@"SavedGame.json"); // Read the file content
            GameStateNode newGameStateNode = JsonConvert.DeserializeObject<GameStateNode>(jsonContent); // Deserialize the JSON content
            return newGameStateNode;
        }
        catch (FileNotFoundException)
        {
            throw new FileNotFoundException($"JSON file not found at path: SavedGame.json");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error reading JSON file: {ex.Message}");
        }
    }

    // Loads dialogues from a JSON file
    public static RootObject LoadDialogue()
    {
        try
        {
            string jsonContent = File.ReadAllText("Dialogues.json"); // Read the file content
            RootObject jsonData = JsonConvert.DeserializeObject<RootObject>(jsonContent); // Deserialize the JSON content
            return jsonData;
        }
        catch (FileNotFoundException)
        {
            throw new FileNotFoundException("JSON file not found at path: Dialogues.json");
        }
        catch (JsonException ex)
        {
            throw new Exception($"Error deserializing JSON file: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error reading JSON file: {ex.Message}");
        }
    }
}