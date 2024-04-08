namespace CMP1903_A2_2324.Games;

public class ThreeOrMore : Game
{
    private static string _rerollMessage = "Two of a pair, would you like to re-roll the remaining die or re-roll all?";
    private static string[] _rerollOptions = ["Re-roll Remaining", "Re-roll All"];
    
    public override void PlayGame(bool shouldOutput)
    {
        base.PlayGame(shouldOutput);
        
        Dice = new DieCollection(5);
        
        while (isPlaying)
        {
            StartTurn();
        }
        EndGame();
    }

    public override void PlayTurn()
    {
        base.PlayTurn();
        Dice.RollAllDie();
        if (ShouldOutput) Dice.OutputDie();

        CheckDie(true);

        if (playerOneScore >= 20 || playerTwoScore >= 20) isPlaying = false;
    }

    void CheckDie(bool canReroll)
    {
        switch (Dice.MostOfAKind())
        {
            case 1:
                GamePrint("No Pairs, Unlucky!");
                break;
            case 2:
                CheckReroll(canReroll);
                break;
            case 3:
                AddScore(3);
                break;
            case 4: 
                AddScore(6);
                break;
            case 5:
                AddScore(12);
                break;
        }
    }

    private void CheckReroll(bool canReroll)
    {
        if (!canReroll)
        {
            if (ShouldOutput) GamePrint("Player cannot re-roll a second time");
            return;
        }

        switch (PlayerChoice(_rerollMessage, _rerollOptions))
        {
            case "Re-roll Remaining":
                Dice.RollSpecificDie(Dice.GetAllDieNotInPairs().ToArray());
                if (ShouldOutput) Dice.OutputDie();
                CheckDie(false);
                break;
            case "Re-roll All":
                Dice.RollAllDie();
                if (ShouldOutput) Dice.OutputDie();
                CheckDie(false);
                break;
        }
    }
}