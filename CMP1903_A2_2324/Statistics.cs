using CMP1903_A2_2324.Games;

namespace CMP1903_A2_2324;

public class Statistics
{

}

public class TurnStats(int turnNo, int playerOneScore, int playerTwoScore)
{
    public int TurnNo = turnNo;
    public int PlayerOneScore = playerOneScore;
    public int PlayerTwoScore = playerTwoScore;

    public override string ToString()
    {
        return $"Turn {TurnNo}, Player One: {playerOneScore}, Player Two: {playerTwoScore}";
    }
}

public class GameStats
{
    private List<TurnStats> _turnStats;

    public GameStats(List<TurnStats> turnStats)
    {
        _turnStats = turnStats;
    }

    public GameStats()
    {
        _turnStats = new();
    }

    public void AddTurn(TurnStats turn)
    {
        _turnStats.Add(turn);
    }

    public void OutputGameStats()
    {
        foreach (TurnStats turn in _turnStats)
        {
            Console.WriteLine(turn);
        }
    }
}