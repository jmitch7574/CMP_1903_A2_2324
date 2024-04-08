namespace CMP1903_A2_2324.Games;

public class SevensOut : Game
{
    public override void PlayGame(bool shouldOutput)
    {
        base.PlayGame(shouldOutput);

        Dice = new DieCollection(2);

        while (isPlaying)
        {
            StartTurn();
        }
        
        GamePrint("Sevens Out!!!");
        EndGame();
    }
    
    public override void PlayTurn()
    {
        base.PlayTurn();
        Dice.RollAllDie();
        
        if (ShouldOutput) Dice.OutputDie();

        if (Dice.DieTotal == 7)
        {
            isPlaying = false;
            return;
        }
            
        if (Dice.DiceItems[0].DieValue == Dice.DiceItems[1].DieValue)
        {
            GamePrint("Doubles! Added score will be doubled");
            AddScore(Dice.DieTotal * 2);
        }
        else AddScore(Dice.DieTotal);
    }
}