namespace CMP1903_A2_2324.Games;

public class SevensOut : Game
{
    public bool doubles { get; private set; }
    
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

        ScoreThisTurn = DieTotal();
        doubles = DiceCollection[0].DieValue == DiceCollection[1].DieValue;
        if (doubles)
        {
            GamePrint("Doubles!, Added score will be doubled");
            ScoreThisTurn *= 2;
        }
        
        // Check if the total of the two die add up to 7 and end the game
        if (DieTotal() == 7)
        {
            IsPlaying = false;
            EndTurn();
            return;
        }
        
        AddScore(ScoreThisTurn);
        
        // End the turn
        EndTurn();
    }
}