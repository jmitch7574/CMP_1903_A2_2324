namespace CMP1903_A2_2324.Games;

public class SevensOut : Game
{
    /// <summary>
    /// Sets up a game of Sevens Out
    /// </summary>
    /// <param name="shouldOutput">Should this game output to console</param>
    public SevensOut(bool shouldOutput) : base(shouldOutput, 2) { }

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

        int score = DieTotal();
        if (DiceCollection[0].DieValue == DiceCollection[1].DieValue)
        {
            GamePrint("Doubles!, Added score will be doubled");
            score *= 2;
        }
        AddScore(score);
        
        // End the turn
        EndTurn();
    }
}