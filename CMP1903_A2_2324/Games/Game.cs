namespace CMP1903_A2_2324.Games;

public class Game
{
    // Player Data
    public int PlayerOneScore { get; protected set; }
    public int PlayerTwoScore { get; protected set; }

    public bool IsPlayerOneAi { get; protected set; }
    public bool IsPlayerTwoAi { get; protected set; }
    
    // Game Data
    public Die[] DiceCollection { get; set; } = new Die[] {};
    public int Turn { get; protected set; }
    public bool IsPlayerOneTurn { get; protected set; }
    public bool IsPlaying { get; protected set; }
    public bool ShouldOutput { get; protected set; }
    public List<int[]> TurnData { get; protected set; } = new List<int[]>();

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

    private static readonly string HumanOrAiM = "Is this player a human or computer?";
    private static readonly string[] HumanOrAiO = ["Human", "Computer"];
    
    

    public static void Main(string[ ] args)
    {
        Game game = new Game();
        game.MainMenu();
    }

    private void MainMenu()
    {
        switch (Menu(_mainMenuM, _mainMenuO))
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

    private void GameMenu()
    {
        switch (Menu(_gameMenuM, _gameOptions))
        {
            case "Sevens Out":
                SevensOut SO = new SevensOut();
                SO.PlayGame(true);
                break;
            case "Three or More":
                ThreeOrMore TOM = new ThreeOrMore();
                TOM.PlayGame(true);
                break;
        }
    }

    private void MakePlayers()
    {
        isPlayerOneAI = Menu("Player 1, " + _humanOrAiM, _humanOrAiO) == "Computer";
        isPlayerTwoAI = Menu("Player 2, " + _humanOrAiM, _humanOrAiO) == "Computer";
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
    /// Contains the base setup for a playable game
    /// </summary>
    /// <param name="playerOne">Player One Object</param>
    /// <param name="playerTwo">Player Two Object</param>
    public virtual void PlayGame(bool shouldOutput)
    {
        MakePlayers();
        playerOneScore = 0;
        playerTwoScore = 0;
        ShouldOutput = shouldOutput;

        Turn = -1;
        isPlaying = true;
    }

    protected void StartTurn()
    {
        Turn++;
        
        GamePrint();

        isPlayerOneTurn = (Turn % 2 == 0);
        
        
        PlayTurn();
    }

    public virtual void PlayTurn()
    { 
        AwaitTurn();
    }

    private void AwaitTurn()
    {
        // Check if current player is a computer player
        bool isAi = (isPlayerOneTurn && isPlayerOneAI) || (!isPlayerOneTurn && isPlayerTwoAI);

        if (isAi)
        {
            Console.WriteLine($"Player {GetCurrentPlayerNumber()}, is taking their turn");
            if (ShouldOutput) Thread.Sleep(3000);
        }
        else
        {
            Console.WriteLine($"Player {GetCurrentPlayerNumber()}, press any key to take your turn");
            Console.ReadKey();
        }
    }

    protected string PlayerChoice(string message, string[] choices)
    {
        // Check if current player is a computer player
        bool isAi = (isPlayerOneTurn && isPlayerOneAI) || (!isPlayerOneTurn && isPlayerTwoAI);

        if (isAi)
        {
            GamePrint($"Player {GetCurrentPlayerNumber()} is making a choice...");
            if (ShouldOutput) Thread.Sleep(2000);
            
            Random random = new Random();
            int choiceIndex = random.Next(choices.Length);
            string choice = choices[choiceIndex];
            Console.WriteLine($"Player {GetCurrentPlayerNumber()} has chosen to {choice}");
            return choice;
        }
        else
        {
            return Menu(message, choices);
        }
    }

    private string GetCurrentPlayerNumber()
    {
        return (isPlayerOneTurn ? "1" : "2");
    }

    protected void AddScore(int score)
    {
        Console.WriteLine($"Player {GetCurrentPlayerNumber()} just added {score} points to their score!");
        if (isPlayerOneTurn)
            playerOneScore += score;
        else
            playerTwoScore += score;
    }
    
    protected void EndGame()
    {
        GamePrint("Game Over");
        if (playerOneScore == playerTwoScore)
        {
            GamePrint($"Tie, both players scored {playerOneScore} points");
        }
        else if (playerOneScore > playerTwoScore)
        {
            Console.WriteLine($"Player 1 Wins with {playerOneScore} points!");
        }
        else
        {
            Console.WriteLine($"Player 2 Wins with {playerTwoScore} points!");
        }
    }

    protected void GamePrint(string message = "")
    {
        if (ShouldOutput) Console.WriteLine(message);
    }
    
}