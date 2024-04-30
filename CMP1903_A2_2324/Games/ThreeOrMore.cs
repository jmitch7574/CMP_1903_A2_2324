namespace CMP1903_A2_2324.Games;
public class ThreeOrMore : Game
{
    // Menu variables
    private static readonly string RerollMessage = "Two of a pair, would you like to re-roll the remaining die or re-roll all?";
    private static readonly string[] RerollOptions = ["Re-roll Remaining", "Re-roll All"];
    
    /// <summary>
    /// Starts a game of Three or More
    /// </summary>
    /// <param name="shouldOutput">Should the game output to console</param>
    public ThreeOrMore(bool shouldOutput) : base(shouldOutput, 5) { }

    /// <summary>
    /// Play a turn of three or more
    /// </summary>
    public override void PlayTurn()
    {
        // Run global turn code from game class
        base.PlayTurn();

        // Check the pairs in our die, allow a reroll
        CheckDie(true);

        // Check if either player is above 20 points and end the game
        if (PlayerOneScore >= 20 || PlayerTwoScore >= 20) IsPlaying = false;
        
        // End turn
        EndTurn();
    }

    /// <summary>
    /// Check for pairs amongst die and scores accordingly
    /// </summary>
    /// <param name="canReroll">Should a reroll be allowed</param>
    void CheckDie(bool canReroll)
    {
        // what is the highest of-a-kind amongst our die collection
        switch (MostOfAKind())
        {
            // No die in pairs
            case 1:
                GamePrint("No Pairs, Unlucky!");
                break;
            // Pairs of die, ask for a reroll if player can reroll
            case 2:
                if (canReroll) AskReroll();
                else GamePrint("Player cannot reroll a second time");
                break;
            // Three of a kind
            case 3:
                AddScore(3);
                break;
            // Four of a kind
            case 4: 
                AddScore(6);
                break;
            // Five of a kind, all die are the same
            case 5:
                AddScore(12);
                break;
        }
    }

    /// <summary>
    /// Ask the player if they want to reroll remaining or all die <br/>
    /// This will reroll all die not in a pairs (e.g if there are two 3s and 2 fives and 1 four, only the 4 will be rerolled
    /// </summary>
    private void AskReroll()
    {
        // Ask the player if they want to reroll
        switch (PlayerChoice(RerollMessage, RerollOptions))
        {
            // Reroll the remaining die and recheck
            case "Re-roll Remaining":
                RollSpecificDie(GetAllDieNotInPairs().ToArray());
                if (ShouldOutput) OutputDie();
                CheckDie(false);
                break;
            // Reroll all die and recheck
            case "Re-roll All":
                RollAllDie();
                if (ShouldOutput) OutputDie();
                CheckDie(false);
                break;
        }
    }
}