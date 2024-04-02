using System.Security.Cryptography;

namespace CMP1903_A2_2324;

public abstract class Player
{
    public int Score { get; private set; } = 0;
    public bool IsComputer { get; set; } = true;
    public string Name { get; set; } = "";

    public void ResetScore()
    {
        Score = 0;
    }

    public void AddScore(int score)
    {
        Score += score;
        Console.WriteLine($"{Name} just added {score} points to their score!");
    }
    
    public virtual void AwaitTurn()
    {
        return;
    }
}