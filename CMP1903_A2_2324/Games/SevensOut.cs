namespace CMP1903_A2_2324.Games;

public class SevensOut : Game
{
    public override void PlayGame(bool shouldOutput)
    {
        base.PlayGame(shouldOutput);

        DiceCollection = CreateDiceCollection(2);
            
        while (IsPlaying)
        {
            StartTurn();
        }
        
        GamePrint("Sevens Out!!!");
        EndGame();
    }
    
    public override void PlayTurn()
    {
        base.PlayTurn();
        RollAllDie();
        
        if (ShouldOutput) OutputDie();

        if (DieTotal() == 7)
        {
            IsPlaying = false;
            EndTurn();
            return;
        }
            
        if (DiceCollection[0].DieValue == DiceCollection[1].DieValue)
        {
            GamePrint("Doubles! Added score will be doubled");
            AddScore(DieTotal() * 2);
        }
        else AddScore(DieTotal());
        
        EndTurn();
    }
}