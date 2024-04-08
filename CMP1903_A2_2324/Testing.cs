using System.Diagnostics;
using CMP1903_A2_2324.Games;

namespace CMP1903_A2_2324;

public class Testing
{
    public static void TestSevensOut()
    {
        SevensOut SO = new SevensOut();
        SO.Dice = new DieCollection(2);
        
        for (int i = 0; i < 1000; i++)
        {
            SO.PlayTurn();
            
            // Debug check that flags if Die total is 7 and isPlaying is not set to false
            bool check = SO.Dice.DieTotal != 7 || (SO.Dice.DieTotal == 7 && !SO.isPlaying);
            Debug.Assert(check, "Game is not ending once sum is 7 or more");
        }
    }
    
    public static void TestThreeOrMore()
    {
        ThreeOrMore TOM = new ThreeOrMore();
        TOM.Dice = new DieCollection(2);
        
        for (int i = 0; i < 1000; i++)
        {
            TOM.PlayTurn();
            
            // Debug check that flags if Die total is 7 and isPlaying is not set to false
            bool check = TOM.playerOneScore < 20 || (TOM.playerOneScore >= 20 && !TOM.isPlaying);
            Debug.Assert(check, "Game is not ending once player score is 20 or more");
        }
    }

    public static void TestDieCollection()
    {
        DieCollection Dice = new DieCollection(10);

        for (int i = 0; i < 1000; i++)
        {
            int sum = 0;
            foreach (Die die in Dice.DiceItems)
            {
                sum += die.DieValue;
            }
            
            Debug.Assert(sum == Dice.DieTotal, "Dice Collection Class incorrectly adding up die values");
        }
    }
}