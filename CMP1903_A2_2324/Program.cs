using System.Text.Json;
using CMP1903_A2_2324.Games;
namespace CMP1903_A2_2324;

public class Program
{
    // Menu Variables
    private static readonly string MainMenuM = "What would you like to do?";
    private static readonly string[] MainMenuO = ["Play Game", "Run Statistics", "Exit"];

    private static readonly string GameMenuM = "What game would you like to play?";
    private static readonly string StatsMenuM = "What statistics would you like to run?";
    private static readonly string[] GameOptions = ["Sevens Out", "Three or More"];
    
    private static bool _isRunning = true;

    private static Statistics _sevensOutStats = new("SevensOut");
    private static Statistics _threeOrMoreStats = new("ThreeOrMore");
    
    private static Dictionary<string, bool> _testData;
    
    /// <summary>
    /// Entry point for program
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[ ] args)
    {
        _testData = Testing.RunTests();
        try
        {
            WriteTests();
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occured writing test log");
            Console.WriteLine(e);
        }
        
        ReadSave();
        
        while (_isRunning) MainMenu();
        
        try
        {
            WriteSave();
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occured writing session to file");
            Console.WriteLine(e);
        }
    }

    /// <summary>
    /// Asks player to choose program operation
    /// </summary>
    /// <returns>
    /// A boolean where true means the program was executed and false means the program should be exited
    /// </returns>
    private static void MainMenu()
    {
        switch (Menu(MainMenuM, MainMenuO))
        {
            case "Play Game":
                GameMenu();
                break;
            case "Run Statistics":
                StatisticsMenu();
                break;
            case "Exit":
                _isRunning = false;
                break;
        }
        
    }

    /// <summary>
    /// Asks player to choose which game to play
    /// </summary>
    private static void GameMenu()
    {
        Game game = new Game(false, 0);
        switch (Menu(GameMenuM, GameOptions))
        {
            case "Sevens Out":
                game = new SevensOut(true);
                game.BeginGame();
                _sevensOutStats.AddGame(game);
                break;
            case "Three or More":
                game = new ThreeOrMore(true);
                game.BeginGame();
                _threeOrMoreStats.AddGame(game);
                break;
        }
    }

    private static void StatisticsMenu()
    {
        Statistics stats = new("Game");
        string statsType = "";
        switch (Menu(StatsMenuM, GameOptions))
        {
            case "Sevens Out":
                statsType = "SevensOut";
                stats = Statistics.StatsSevensOut();
                break;
            case "Three or More":
                statsType = "ThreeOrMore";
                stats = Statistics.StatsThreeOrMore();
                break;
        }
        
        WriteStats(stats);
    }
    
    /// <summary>
    /// Creates a menu for selecting an option
    /// </summary>
    /// <param name="message">The message that should be displayed with the options</param>
    /// <param name="options">A list of options the user can choose from</param>
    /// <returns></returns>
    public static string Menu(string message, string[] options)
    {
        while (true)
        {
            // WriteInfo message and a new line gap
            Console.WriteLine();
            Console.WriteLine($"{message}");
            
            // Go through all our options
            for (int i = 0; i < options.Length; i++)
            {
                // WriteLine for each option
                Console.WriteLine($"{i+1}. {options[i]}");
            }

            // Input selection
            char selection = Console.ReadKey().KeyChar;
            
            // One liner to try parse an int, repeat loop if it can't be parsed
            if (!int.TryParse(selection.ToString(), out int selectionInt)) continue;

            // Check if selection is within valid range
            if (selectionInt < 1 || selectionInt > options.Length) continue;
            
            Console.WriteLine();
            // return option in single element array
            // add -1 because our options array is always plus one
            return options[selectionInt-1];
            

        }
    }

    /// <summary>
    /// Asks the user to input an integer
    /// </summary>
    /// <returns>An Integer input</returns>
    public static int IntInput()
    {
        // Create value to hold integer value
        int intInput = 0;
        
        // Loop until valid value inputted
        bool validIntInput = false;
        while (!validIntInput)
        {
            // Get raw input
            string input = Console.ReadLine();
            
            // Check if input is valid integer
            validIntInput = int.TryParse(input, out intInput);
            
            // Send error if not
            if (!validIntInput) Console.WriteLine("Please enter a valid number");
        }

        // Return inputted integer]
        return intInput;
    }
    
    //------------------------------------ FILE MANAGING --------------------------------------------
    
    /// <summary>
    /// Generic Json Serializer
    /// </summary>
    /// <param name="data">The data to be serialized</param>
    /// <typeparam name="T">The type of the serialized data</typeparam>
    /// <returns>a JSON string of the data</returns>
    private static string SerializeData<T>(T data)
    {
        string gameJson = JsonSerializer.Serialize(data);
        return gameJson;
    }

    /// <summary>
    /// Deserializes a json string back into data
    /// </summary>
    /// <param name="fileJson">The data to be deserialized</param>
    /// <typeparam name="T">The data type</typeparam>
    /// <returns>The deserialized data</returns>
    /// <exception cref="JsonException">Thrown when json data is not valid <see cref="T"/> data</exception>
    public static T DeserializeData<T>(string fileJson)
    {
        // Deserialize the data
        T? data = JsonSerializer.Deserialize<T>(fileJson);
        // If data was not valid
        if (data == null)
            throw new JsonException("JSON data is invalid");
        // Return data
        return data;
    }

    /// <summary>
    /// Reads the contents of a file
    /// </summary>
    /// <param name="filePath">The path to the file</param>
    /// <returns>file data as a string</returns>
    /// <exception cref="FileNotFoundException">The file was not found</exception>
    /// <exception cref="FileLoadException">The file could not be loaded</exception>
    public static string ReadFileContents(string filePath)
    {
        if (!File.Exists(filePath)) throw new FileNotFoundException("File does not exist");
        try
        {
            string file = File.ReadAllText(filePath);
            return file;
        }
        catch
        {
            throw new FileLoadException("File could not be loaded");
        }
    }

    /// <summary>
    /// Writes contents to a file
    /// </summary>
    /// <param name="filePath">Path of the file</param>
    /// <param name="fileContents">What should be written</param>
    /// <exception cref="FileLoadException">If the file could not be loaded or created</exception>
    private static void WriteFileContents(string filePath, string fileContents)
    {
        try
        {
            File.WriteAllText(filePath, fileContents);
        }
        catch
        {
            throw new FileLoadException("File could not be written");
        }
        
    }

    /// <summary>
    /// Gets file in directory
    /// </summary>
    /// <param name="dirPath">Directory path</param>
    /// <returns></returns>
    /// <exception cref="DirectoryNotFoundException">Directory cannot be found</exception>
    /// <exception cref="FileLoadException">Directory could not be loaded</exception>
    /// <returns>An array of file paths within <see cref="dirPath"/></returns>
    public static string[] GetFilesInDirectory(string dirPath)
    {
        if (!Directory.Exists(dirPath)) throw new DirectoryNotFoundException("Directory could not be found");

        try
        {
            string[] files = Directory.EnumerateFiles(dirPath).ToArray();
            return files;
        }
        catch
        {
            throw new FileLoadException("Files in directory could not be read");
        }
    }

    /// <summary>
    /// Writes <see cref="_stats"/> to file
    /// </summary>
    private static void WriteStats(Statistics stats)
    {
        // Check the file saving directory exists
        if (!Directory.Exists("StatsData/")) Directory.CreateDirectory("StatsData");
        
        // Generate a file name and file path
        string filePath = $"StatsData/Session-{DateTime.Now:yyyyMMddHHmmss}.json";
        
        // Serialize statistics object and write to file
        WriteFileContents(filePath, SerializeData(stats));   
        
        // Output success message
        Console.WriteLine($"Statistics data written to {filePath}");
    }

    private static void ReadSave()
    {
        // Generate a file name and file path
        string sevensOutFilePath = $"SaveData/SevensOut.json";
        string threeOrMoreFilePath = $"SaveData/ThreeOrMore.json";

        try
        {
            _sevensOutStats = DeserializeData<Statistics>(ReadFileContents(sevensOutFilePath));
        }
        catch
        {
            _sevensOutStats = new("SevensOut");
        }

        try
        {
            _threeOrMoreStats = DeserializeData<Statistics>(ReadFileContents(threeOrMoreFilePath));
        }
        catch
        {
            _threeOrMoreStats = new("ThreeOrMore");
        }
    }
    private static void WriteSave()
    {
        // Check the file saving directory exists
        if (!Directory.Exists("SaveData/")) Directory.CreateDirectory("SaveData");
        
        // Generate a file name and file path
        string sevensOutFilePath = $"SaveData/SevensOut.json";
        string threeOrMoreFilePath = $"SaveData/ThreeOrMore.json";
        
        // Serialize statistics object and write to file
        WriteFileContents(sevensOutFilePath, SerializeData(_sevensOutStats));   
        WriteFileContents(threeOrMoreFilePath, SerializeData(_threeOrMoreStats));   
        
        // Output success message
        Console.WriteLine($"Save data written");
    }

    /// <summary>
    /// Writes <see cref="_testData"/> to file
    /// </summary>
    private static void WriteTests()
    {
        // Check the file saving directory exists
        if (!Directory.Exists("TestsData/")) Directory.CreateDirectory("TestsData");
        
        // Generate a file name and file path
        string filePath = $"TestsData/Tests-{DateTime.Now:yyyyMMddHHmmss}.json";
        
        // Serialize statistics object and write to file
        WriteFileContents(filePath, SerializeData(_testData));   
        
        // Output success message
        Console.WriteLine($"Test Log written to {filePath}");
    }
}