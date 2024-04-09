namespace CMP1903_A2_2324.Games;

public class ThreeOrMore : Game
{
    private static readonly string RerollMessage = "Two of a pair, would you like to re-roll the remaining die or re-roll all?";
    private static readonly string[] RerollOptions = ["Re-roll Remaining", "Re-roll All"];
    
    public override void PlayGame(bool shouldOutput)
    {
        base.PlayGame(shouldOutput);

        DiceCollection = CreateDiceCollection(5);
        
        while (IsPlaying)
        {
            StartTurn();
        }
        EndGame();
    }

    public override void PlayTurn()
    {
        base.PlayTurn();
        RollAllDie();
        if (ShouldOutput) OutputDie();

        CheckDie(true);

        if (PlayerOneScore >= 20 || PlayerTwoScore >= 20) IsPlaying = false;
        
        EndTurn();
    }

    void CheckDie(bool canReroll)
    {
        switch (MostOfAKind())
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

        switch (PlayerChoice(RerollMessage, RerollOptions))
        {
            case "Re-roll Remaining":
                RollSpecificDie(GetAllDieNotInPairs().ToArray());
                if (ShouldOutput) OutputDie();
                CheckDie(false);
                break;
            case "Re-roll All":
                RollAllDie();
                if (ShouldOutput) OutputDie();
                CheckDie(false);
                break;
        }
    }
}