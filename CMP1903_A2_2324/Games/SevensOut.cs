namespace CMP1903_A2_2324.Games;

public class SevensOut : Game
{
    /// <summary>
    /// Sets up a game of Sevens Out
    /// </summary>
    /// <param name="shouldOutput">Should this game output to console</param>
    public override void PlayGame(bool shouldOutput)
    {
        // Initiate the game from the game class
        base.PlayGame(shouldOutput);

        // Create a dice collection
        DiceCollection = CreateDiceCollection(2);
            
        // Keep playing turns until the game ends
        while (IsPlaying)
        {
            StartTurn();
        }
        
        // Print message and end game
        GamePrint("Sevens Out!!!");
        EndGame();
    }
    
    /// <summary>
    /// Plays a turn of SevensOut
    /// </summary>
    public override void PlayTurn()
    {
        // Runs base code from game class
        base.PlayTurn();
        
        // Rolls all dice
        RollAllDie();
        
        // Output the dice
        if (ShouldOutput) OutputDie();

        // Check if the total of the two die add up to 7 and end the game
        if (DieTotal() == 7)
        {
            IsPlaying = false;
            EndTurn();
            return;
        }
            
        // Check if the die are equal and double the score
        if (DiceCollection[0].DieValue == DiceCollection[1].DieValue)
        {
            GamePrint("Doubles! Added score will be doubled");
            AddScore(DieTotal() * 2);
        }
        // Otherwise just add normal score
        else AddScore(DieTotal());
        
        // End the turn
        EndTurn();
    }
}