namespace CMP1903_A2_2324.Games;

public class Game
{
    // These are all marked as properties because C#s built in JSON serializer cannot read variables for some reason :(
    
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
    public List<int[]> TurnScores { get; protected set; } = new List<int[]>();
    public List<int[]> TurnRolls { get; protected set; } = new List<int[]>();

    public static List<Game> Session { get; protected set; } = new();


    // Menu variables
    private static readonly string HumanOrAiM = "Is this player a human or computer?";
    private static readonly string[] HumanOrAiO = ["Human", "Computer"];
    
    // ---------------------------- GAME FUNCTIONS -------------------------------------
    
    /// <summary>
    /// Contains the base setup for a playable game
    /// </summary>
    /// <param name="shouldOutput">Should this game print to console.</param>
    public Game(bool shouldOutput, int diceSize)
    {
        MakePlayers();
        PlayerOneScore = 0;
        PlayerTwoScore = 0;
        ShouldOutput = shouldOutput;

        Turn = -1;
        TurnScores = new List<int[]>();
        IsPlaying = true;
        
        // Create dice collection
        DiceCollection = CreateDiceCollection(diceSize);
        
        // keep running turns while game is still playing
        while (IsPlaying)
        {
            StartTurn();
        }
        
        // End game
        EndGame();
    }
    
    /// <summary>
    /// Asks the user whether or not each player is a human or robot
    /// </summary>
    private void MakePlayers()
    {
        IsPlayerOneAi = Program.Menu("Player 1, " + HumanOrAiM, HumanOrAiO) == "Computer";
        IsPlayerTwoAi = Program.Menu("Player 2, " + HumanOrAiM, HumanOrAiO) == "Computer";
    }
    
    /// <summary>
    /// Runs at the start of each turn. Determines which player's turn it is and then plays the turn
    /// </summary>
    protected void StartTurn()
    {
        Turn++;
        
        GamePrint();

        IsPlayerOneTurn = (Turn % 2 == 0);
        
        
        PlayTurn();
    }

    /// <summary>
    /// Base functionality for a turn, games add their turn functionality.
    /// <see cref="AwaitTurn"/> waits for player confirmation to begin turn
    /// </summary>
    public virtual void PlayTurn()
    { 
        AwaitTurn();
    }

    /// <summary>
    /// Runs at the end of the turn and adds current game stats to our Turn Data list for <see cref="Statistics"/> purposes
    /// </summary>
    protected void EndTurn()
    {
        TurnData.Add(new int[3] {Turn, PlayerOneScore, PlayerTwoScore});
        TurnScores.Add(new int[3] {Turn, PlayerOneScore, PlayerTwoScore});
        TurnRolls.Add(GetDieValues());
    }

    /// <summary>
    /// Function that runs at the end of the game. 
    /// Outputs the winner and saves game data to file
    /// </summary>
    protected void EndGame()
    {
        GamePrint("Game Over");
        if (PlayerOneScore == PlayerTwoScore)
        {
            GamePrint($"Tie, both players scored {PlayerOneScore} points");
        }
        else if (PlayerOneScore > PlayerTwoScore)
        {
            Console.WriteLine($"Player 1 Wins with {PlayerOneScore} points!");
        }
        else
        {
            Console.WriteLine($"Player 2 Wins with {PlayerTwoScore} points!");
        }

        try
        {
            Statistics.WriteGameToFile(this);
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occured writing game to file");
            //Console.WriteLine(e);
        }
    }
    
    
    // ------------------------------- PLAYER FUNCTIONS ----------------------------------
    // i would have preferred to have a separate player class that included these functions
    // it'd make things a lot more self contained and neater in my opinion
    // still works either way so ¯\_(ツ)_/¯
    
    /// <summary>
    /// Gets player confirmation to start their turn in the form of a readkey operation.
    /// If the player is a computer player, instead just wait 2 seconds and continue
    /// </summary>
    private void AwaitTurn()
    {
        // Check if current player is a computer player
        bool isAi = (IsPlayerOneTurn && IsPlayerOneAi) || (!IsPlayerOneTurn && IsPlayerTwoAi);

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
    
    /// <summary>
    /// Ask the player to choose an option using our <see cref="Menu"/> function.
    /// If the player is a computer, instead make a <see cref="Random"/> choice
    /// </summary>
    /// <param name="message">The message that should be output with the options</param>
    /// <param name="choices">A list of options the player can choose</param>
    /// <returns>The option the player chose</returns>
    protected string PlayerChoice(string message, string[] choices)
    {
        // Check if current player is a computer player
        bool isAi = (IsPlayerOneTurn && IsPlayerOneAi) || (!IsPlayerOneTurn && IsPlayerTwoAi);

        if (isAi)
        {
            Console.WriteLine(message);
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
            return Program.Menu(message, choices);
        }
    }

    /// <summary>
    /// Get the current players number
    /// </summary>
    /// <returns>The player's number</returns>
    private string GetCurrentPlayerNumber()
    {
        return (IsPlayerOneTurn ? "1" : "2");
    }
    
    /// <summary>
    /// Adds score to the current player
    /// </summary>
    /// <param name="score">how many points should be added to the player's score</param>
    protected void AddScore(int score)
    {
        Console.WriteLine($"Player {GetCurrentPlayerNumber()} just added {score} points to their score!");
        if (IsPlayerOneTurn)
            PlayerOneScore += score;
        else
            PlayerTwoScore += score;
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
    /// Gets die values as an array
    /// </summary>
    /// <returns></returns>
    public int[] GetDieValues()
    {
        int[] values = new int[DiceCollection.Length];
        for (int i = 0; i < DiceCollection.Length; i++)
        {
            values[i] = DiceCollection[i].DieValue;
        }

        return values;
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