using System.Diagnostics;
using CMP1903_A2_2324.Games;

namespace CMP1903_A2_2324;

public class Testing
{
    public static void TestSevensOut()
    {
        SevensOut so = new SevensOut();
        so.DiceCollection = new Die[2];
        
        for (int i = 0; i < 1000; i++)
        {
            so.PlayTurn();
            
            // Debug check that flags if Die total is 7 and isPlaying is not set to false
            bool check = so.DieTotal() != 7 || (so.DieTotal() == 7 && !so.IsPlaying);
            Debug.Assert(check, "Game is not ending once sum is 7 or more");
        }
    }
    
    public static void TestThreeOrMore()
    {
        ThreeOrMore tom = new ThreeOrMore();
        tom.DiceCollection = new Die[5];
        
        for (int i = 0; i < 1000; i++)
        {
            tom.PlayTurn();
            
            // Debug check that flags if Die total is 7 and isPlaying is not set to false
            bool check = tom.PlayerOneScore < 20 || (tom.PlayerOneScore >= 20 && !tom.IsPlaying);
            Debug.Assert(check, "Game is not ending once player score is 20 or more");
        }
    }

    public static void TestDieCollection()
    {
        Game game = new Game();
        game.DiceCollection = new Die[10];
        

        for (int i = 0; i < 1000; i++)
        {
            game.RollAllDie();
            
            int sum = 0;
            foreach (Die die in game.DiceCollection)
            {
                sum += die.DieValue;
            }
            
            Debug.Assert(sum == game.DieTotal(), "Dice Collection Class incorrectly adding up die values");
        }
    }
}