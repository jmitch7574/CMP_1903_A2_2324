using System.Text.Json;
using CMP1903_A2_2324.Games;

namespace CMP1903_A2_2324;

public class Statistics
{
    // ---------------------- PROPERTIES -----------------------
    public int GamesPlayed => Session.Count;
    public int AvgTurnsPerGame => GetAvgTurnsInSession();

    public int HighScore => GetHighestScoreInSession();

    public int[] AllRolls => GetAllRollsInSession();
    public int PlayerOneWins { get; set; }
    public int PlayerTwoWins { get; set; }
    public int Ties { get; set; }

    public List<Game> Session { get; set; } = new();
    
    // ---------------------- RUNNING GAMES --------------------
    public static void RunGame()
    {
        for (int i = 0; i < 1000000; i++)
        {
            Game game = new SevensOut(false);
        }
    }
    
    // ---------------------- FILE MANAGING --------------------
    private static string SerializeSession(Statistics session)
    {
        string gameJson = JsonSerializer.Serialize(session);
        return gameJson;
    }

    public static Statistics DeserializeSession(string fileJson)
    {
        Statistics? games = JsonSerializer.Deserialize<Statistics>(fileJson);
        if (games == null)
            throw new JsonException("JSON data is invalid");
        return games;
    }

    public static string ReadFileContents(string filePath)
    {
        if (!File.Exists(filePath)) throw new FileNotFoundException("File does not exist");
        try
        {
            string file = File.ReadAllText(filePath);
            return file;
        }
        catch
        {
            throw new FileLoadException("File could not be loaded");
        }
    }

    private static void WriteFileContents(string filePath, string fileContents)
    {
        try
        {
            File.WriteAllText(filePath, fileContents);
        }
        catch
        {
            throw new FileLoadException("File could not be written");
        }
        
    }

    public static string[] GetFilesInDirectory(string dirPath)
    {
        if (!Directory.Exists(dirPath)) throw new DirectoryNotFoundException("Directory could not be found");

        try
        {
            string[] files = Directory.EnumerateFiles(dirPath).ToArray();
            return files;
        }
        catch
        {
            throw new FileLoadException("Files in directory could not be read");
        }
    }

    public void WriteStatsToFile()
    {
        (PlayerOneWins, PlayerTwoWins, Ties) = GetPlayerWinsInSession();
        
        if (!Directory.Exists("Games/")) Directory.CreateDirectory("Games");
        string filePath = $"Games/Session-{DateTime.Now:yyyyMMddHHmmss}.json";
        WriteFileContents(filePath, SerializeSession(this));   
        Console.WriteLine($"Game contents written to {filePath}");
    }
    
    public int GetHighestScoreInSession()
    {
        int currentHighest = 0;
        foreach (Game game in Session)
        {
            if (game.PlayerOneScore > currentHighest) currentHighest = game.PlayerOneScore;
            if (game.PlayerTwoScore > currentHighest) currentHighest = game.PlayerTwoScore;
        }

        return currentHighest;
    }

    public (int, int, int) GetPlayerWinsInSession()
    {
        int playerOneWins = 0;
        int playerTwoWins = 0;
        int ties = 0;

        foreach (Game game in Session)
        {
            if (game.PlayerOneScore > game.PlayerTwoScore) playerOneWins++;
            else if (game.PlayerTwoScore > game.PlayerOneScore) playerTwoWins++;
            else ties++;
        }

        return (playerOneWins, playerTwoWins, ties);
    }

    public int[] GetAllRollsInSession()
    {
        List<int> rolls = new();
        foreach (Game game in Session)
        {
            foreach (int[] rollData in game.TurnRolls)
            {
                rolls.AddRange(rollData);
            }
        }

        return rolls.ToArray();
    }

    public int GetAvgTurnsInSession()
    {
        if (Session.Count == 0) return 0;
        
        int totalTurns = 0;
        foreach (Game game in Session)
        {
            totalTurns += game.TurnRolls.Count;
        }

        return totalTurns / Session.Count;
    }
}