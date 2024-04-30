using System.Text.Json;
using CMP1903_A2_2324.Games;

namespace CMP1903_A2_2324;

public class Statistics
{
    // ---------------------- PROPERTIES -----------------------
    
    // List of games in session
    public string GameType { get; set; }
    public int GamesPlayed { get; set; }
    public double AvgTurnsPerGame { get; set; }
    public int HighestTurnCount { get; set; }
    public int HighScore { get; set; }
    public int PlayerOneWins { get; set; }
    public int PlayerTwoWins { get; set; }
    public int Ties { get; set; }

    public Statistics(string gameType)
    {
        this.GameType = gameType;
        this.GamesPlayed = 0;
        this.AvgTurnsPerGame = 0;
        this.HighestTurnCount = 0;
        this.HighScore = 0;
        this.PlayerOneWins = 0;
        this.PlayerTwoWins = 0;
        this.Ties = 0;
    }
    
    // ---------------------- RUNNING GAMES --------------------
  
    public static Statistics StatsSevensOut()
    {
        Statistics stats = new("SevensOut");
        
        Console.WriteLine("How many times should the game be run?");
        int runCount = Program.IntInput();
        
        for (int i = 0; i < runCount; i++)
        {
            Game game = new SevensOut(false);
            game.BeginGame();
            stats.AddGame(game);
        }

        return stats;
    }
    public static Statistics StatsThreeOrMore()
    {
        Statistics stats = new("ThreeOrMore");
        Console.WriteLine("How many times should the game be run?");
        int runCount = Program.IntInput();
        
        for (int i = 0; i < runCount; i++)
        {
            Game game = new ThreeOrMore(false);
            game.BeginGame();
            stats.AddGame(game);
        }

        return stats;
    }


    public void AddGame(Game game)
    {
        double totalTurns = this.AvgTurnsPerGame * this.GamesPlayed;
        totalTurns += game.Turn;
        this.AvgTurnsPerGame = totalTurns / ++this.GamesPlayed;

        switch (game.GameResult)
        {
            case "1":
                this.PlayerOneWins++;
                break;
            case "2":
                this.PlayerTwoWins++;
                break;
            case "3":
                this.Ties++;
                break;
        }

        if (game.Turn > this.HighestTurnCount) this.HighestTurnCount = game.Turn;

        if (game.PlayerOneScore > this.HighScore) this.HighScore = game.PlayerOneScore;
        if (game.PlayerTwoScore > this.HighScore) this.HighScore = game.PlayerTwoScore;
    }
}