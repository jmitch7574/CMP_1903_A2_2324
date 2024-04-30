using System.Diagnostics;
using CMP1903_A2_2324.Games;

namespace CMP1903_A2_2324;

public static class Testing
{

    public static Dictionary<string, bool> RunTests()
    {
        Dictionary<string, bool> testResults = new();
        testResults["DieTotalling"] = TestDieTotalling();
        testResults["DiceRollInRange"] = TestDiesInRange();
        testResults["SevensOutEnding"] = TestSevensOutEnding();
        testResults["SevensOutDoubling"] = TestSevensOutScoreDouble();
        testResults["ThreeOrMoreEnding"] = TestThreeOrMoreEnding();

        return testResults;
    }
    
    
    // ---------------------------- DIE CLASS TESTING ------------------------------------------------------------------
    private static bool TestDieTotalling()
    {
        Game game = new Game(false, 10);
        
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

        return true;
    }

    private static bool TestDiesInRange()
    {
        Die die = new Die();

        for (int i = 0; i < 1000; i++)
        {
            die.Roll();

            Debug.Assert(die.DieValue >= 1, "Die has rolled outside lower range");
            Debug.Assert(die.DieValue <= 6, "Die has rolled outside upper range");
        }

        return true;
    }
    
    
    // ------------------------------------ SEVENS OUT TESTING -----------------------------------------
    private static bool TestSevensOutEnding()
    {
        SevensOut so = new SevensOut(false);
        
        for (int i = 0; i < 1000; i++)
        {
            so.PlayTurn();
            
            // Debug check that flags if Die total is 7 and isPlaying is not set to false
            bool check = so.DieTotal() != 7 || (so.DieTotal() == 7 && !so.IsPlaying);
            Debug.Assert(check, "Game is not ending once sum is 7 or more");
        }
        
        return true;
    }

    private static bool TestSevensOutScoreDouble()
    {
        SevensOut so = new SevensOut(false);
        
        for (int i = 0; i < 1000; i++)
        {
            so.PlayTurn();
            
            // Debug check that flags if Die total is 7 and isPlaying is not set to false
            int dice1 = so.DiceCollection[0].DieValue;
            int dice2 = so.DiceCollection[1].DieValue;
            
            bool doubles = so.DiceCollection[0].DieValue == so.DiceCollection[1].DieValue;
            bool doublesCheck = doubles == so.doubles;
            int score = so.DiceCollection[0].DieValue + so.DiceCollection[1].DieValue;
            int doubledScore = score *2;
            bool scoreDoubledCheck = (so.doubles && so.ScoreThisTurn == doubledScore) ||
                                     (!so.doubles && so.ScoreThisTurn == score);
            
            Debug.Assert(doublesCheck, "Sevens Out not correctly checking for doubles");
            Debug.Assert(scoreDoubledCheck, "Sevens out not correctly doubling score");

        }
        
        return true;
    }
    
    // ------------------------------- Three or More Testing ------------------------------------------
    private static bool TestThreeOrMoreEnding()
    {
        ThreeOrMore tom = new ThreeOrMore(false);
        
        for (int i = 0; i < 1000; i++)
        {
            tom.PlayTurn();
            
            // Debug check that flags if Die total is 7 and isPlaying is not set to false
            bool check = tom.PlayerOneScore < 20 || (tom.PlayerOneScore >= 20 && !tom.IsPlaying);
            Debug.Assert(check, "Game is not ending once player score is 20 or more");
        }
        
        return true;
    }
}