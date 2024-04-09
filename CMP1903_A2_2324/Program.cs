using CMP1903_A2_2324.Games;
namespace CMP1903_A2_2324;

public class Program
{
    // Menu Variables
    private static readonly string MainMenuM = "What would you like to do?";
    private static readonly string[] MainMenuO = ["Play Game", "Run Statistics", "Run Testing"];

    private static readonly string GameMenuM = "What game would you like to play?";
    private static readonly string[] GameOptions = ["Sevens Out", "Three or More"];

    private static readonly string StatsMenuM = "What statistics would you like to run?";
    private static readonly string TestMenuM = "What would you like to Test?";
    private static readonly string[] TestAndStatsOptions = ["Sevens Out", "Three or More"];
    
    private static readonly string StatisticsM = "Would you like to see match statistics?";
    private static readonly string[] StatisticsO = ["Yes", "No"];

    
    

    /// <summary>
    /// Entry point for program
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[ ] args)
    {
        MainMenu();
    }

    
    private static void MainMenu()
    {
        switch (Menu(MainMenuM, MainMenuO))
        {
            case "Play Game":
                GameMenu();
                break;
            case "Run Statistics":
                break;
            case "Run Testing":
                break;
        }
    }

    private static void GameMenu()
    {
        switch (Menu(GameMenuM, GameOptions))
        {
            case "Sevens Out":
                SevensOut so = new SevensOut();
                so.PlayGame(true);
                break;
            case "Three or More":
                ThreeOrMore tom = new ThreeOrMore();
                tom.PlayGame(true);
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
}