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
    
    /// <summary>
    /// Create a new <see cref="Die"/> array and fill it with empty <see cref="Die"/> objects
    /// </summary>
    /// <param name="count"></param>
    /// <returns>A Die Array</returns>
    protected Die[] CreateDiceCollection(int count)
    {
        Die[] die = new Die[count];
        for (int i = 0; i < count; i++)
            die[i] = new Die();

        return die;
    }
    
    /// <summary>
    /// A custom function that saves repeating if(shouldOutput) everytime i wanna write something to the console.
    /// Get that DRY programming in.
    /// </summary>
    /// <param name="message">Message to be outputted</param>
    protected void GamePrint(string message = "")
    {
        if (ShouldOutput) Console.WriteLine(message);
    }
    
    /// <summary>
    /// Sums the Die in <see cref="DiceCollection"/>
    /// </summary>
    /// <returns>The sum of the die in <see cref="DiceCollection"/></returns>
    public int DieTotal()
    {
        return DiceCollection.Sum(die => die.DieValue);
    }
    
    /// <summary>
    /// Rolls all the die in <see cref="DiceCollection"/>
    /// </summary>
    public void RollAllDie()
    {
        foreach (Die die in DiceCollection)
        {
            die.Roll();
        }
    }

    /// <summary>
    /// Calculates the highest-of-a-kind set. Used to calculate score in <see cref="ThreeOrMore"/>
    /// </summary>
    /// <example>
    /// In a set of die with values [1, 2, 2, 4, 4, 4, 5]
    /// The highest-of-a-kind would be 3, as there are three 4s
    /// </example>
    /// <returns>The highest ofa kind</returns>
    public int MostOfAKind()
    {
        int currentHighest = 0;
        
        // Go through the DiceDictionary
        // DiceDictionary formats dice data as { DiceValue : ArrayOfIndexesOfDice }
        foreach (KeyValuePair<int, List<int>> entry in AsDictionary())
        {
            // Get how many Dice have current value
            // If its higher than the current highest then update current highest
            if (entry.Value.Count > currentHighest)
            {
                currentHighest = entry.Value.Count;
            }
        }

        return currentHighest;
    }
    
    /// <summary>
    /// Gets the index of every dice in <see cref="DiceCollection"/> where at least one other dice shares its value
    /// </summary>
    /// <example>
    /// In a set of dice with values [1, 2, 2, 4, 4, 4, 5] <br/>
    /// It would return the indexes of all die with value 2 or 4 as they have other die with the same value. <br/>
    /// It does not return the indexes of die with value 1 and 5 as no other dice share that value. 
    /// </example>
    /// <returns></returns>
    public List<int> GetAllDieInPairs()
    {
        // Instantiate List
        List<int> allDieInPairs = [];
        
        // Go through Dice Dictionary
        foreach (List<int> list in AsDictionary().Values)
        {
            // If there is more than one dice that share a given value
            if (list.Count > 1)
            {
                // Add all die with shared value to list
                allDieInPairs.AddRange(list);
            }
        }

        // Return list
        return allDieInPairs;
    }
    
    /// <summary>
    /// Does the opposite of <see cref="GetAllDieInPairs"/> <br/>
    /// jk, Returns all die that do not share their value with other die
    /// </summary>
    /// <example>
    /// In a set of dice with values [1, 2, 2, 4, 4, 4, 5] <br/>
    /// It would return the indexes of all die with value 1 or 5 as no other die share that value. <br/>
    /// It does not return the indexes of die with value 2 or 4 as other die share those values.
    /// </example>
    /// <returns>All die that do not share their value </returns>
    public List<int> GetAllDieNotInPairs()
    {
        // Get all die in pairs
        List<int> allDieInPairs = GetAllDieInPairs();
        List<int> allDieNotInPairs = [];
        
        // Go through each die
        foreach(Die die in DiceCollection)
        {
            // Check if it is in GetAllDieInPairs()
            int dieIndex = Array.IndexOf(DiceCollection, die);
            if (!allDieInPairs.Contains(dieIndex))
            {
                // add it to list
                allDieNotInPairs.Add(dieIndex);
            }
        }

        // return list
        return allDieNotInPairs;
    }
    
    /// <summary>
    /// Converts our dice collection into a data dictionary, where the Keys are possible dice values 1-6.
    /// And value is an <see cref="Array"/> of indexes of <see cref="Die"/> in <see cref="DiceCollection"/> that have rolled that value
    /// </summary>
    /// <returns>A data dictionary</returns>
    public Dictionary<int, List<int>> AsDictionary()
    {
        // Initiate for loop
        Dictionary<int, List<int>> pairSet = new();
        
        // For all possible die values 1-6
        for (int i = 1; i <= 6; i++)
        {
            // Add the indexes of all die that match die value
            pairSet[i] = FindMatches(i);
        }

        return pairSet;
    }
    
    /// <summary>
    /// Get indexes of all <see cref="Die"/> in <see cref="DiceCollection"/> that match <see cref="target"/>
    /// </summary>
    /// <param name="target">The target to match</param>
    /// <returns>An array of indexes</returns>
    public List<int> FindMatches(int target)
    {
        List<int> matches = [];
        
        // Use LINQ to get all die that match our target
        matches.AddRange(
            from die in DiceCollection
            where die.DieValue == target 
            select Array.IndexOf(DiceCollection, die));

        return matches;
    }
    
    /// <summary>
    /// Prints all <see cref="Die"/> in <see cref="DiceCollection"/> to the console
    /// </summary>
    public void OutputDie()
    {
        foreach (Die die in DiceCollection)
        {
            Console.WriteLine(die);
        }
    }

    /// <summary>
    /// Given an array of indexes of <see cref="Die"/> in <see cref="DiceCollection"/>. Roll those specific Dice
    /// </summary>
    /// <param name="indexes">The indexes to roll</param>
    public void RollSpecificDie(int[] indexes)
    {
        foreach (int index in indexes)
        {
            DiceCollection[index].Roll();
        }
    }
}