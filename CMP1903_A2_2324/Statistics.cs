using System.Text.Json;
using CMP1903_A2_2324.Games;

namespace CMP1903_A2_2324;

public static class Statistics
{
    // ---------------------- FILE MANAGING --------------------
    private static string SerializeGameList(List<Game> game)
    {
        string gameJson = JsonSerializer.Serialize(game);
        return gameJson;
    }

    public static List<Game> DeserializeGameList(string fileJson)
    {
        List<Game>? games = JsonSerializer.Deserialize<List<Game>>(fileJson);
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

    public static void WriteGameToFile(Game game)
    {
        if (!Directory.Exists("Games/")) Directory.CreateDirectory("Games");
        string filePath = $"Games/{game.GetType()}-{DateTime.Now:yyyyMMddHHmmss}.json";
        WriteFileContents(filePath, SerializeGameList([game]));  
        Console.WriteLine($"Game contents written to {filePath}");
    }

    public static void WriteGameToFile(List<Game> games)
    {
        if (!Directory.Exists("Games/")) Directory.CreateDirectory("Games");
        string filePath = $"Games/{games[0].GetType()}-{DateTime.Now:yyyyMMddHHmmss}.json";
        WriteFileContents(filePath, SerializeGameList(games));   
        Console.WriteLine($"Game contents written to {filePath}");
    }
}