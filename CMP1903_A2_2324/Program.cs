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

    public static Statistics stats = new();
    
    /// <summary>
    /// Entry point for program
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[ ] args)
    {
        Testing.TestDieCollection();
        Testing.TestSevensOut();
        Testing.TestThreeOrMore();
        
        // Reset session data so it is not affected by testing
        stats = new Statistics();
        
        while (_isRunning) MainMenu();
        
        try
        {
            stats.WriteStatsToFile();
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
                break;
            case "Three or More":
                game = new ThreeOrMore(true);
                break;
        }
        game.BeginGame();
        stats.Session.Add(game);
    }

    private static void StatisticsMenu()
    {
        switch (Menu(StatsMenuM, GameOptions))
        {
            case "Sevens Out":
                stats.Session.AddRange(Statistics.StatsSevensOut());
                break;
            case "Three or More":
                stats.Session.AddRange(Statistics.StatsThreeOrMore());
                break;
        }
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

    public static int IntInput()
    {
        bool validIntInput = false;
        int intInput = 0;
        while (!validIntInput)
        {
            string input = Console.ReadLine();
            validIntInput = int.TryParse(input, out intInput);
            if (!validIntInput) Console.WriteLine("Please enter a valid number");
        }

        return intInput;
    }
}