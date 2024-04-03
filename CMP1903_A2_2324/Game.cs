namespace CMP1903_A2_2324;

public abstract class Game
{
    static public Player PlayerOne;
    static public Player PlayerTwo;
    
    /// <summary>
    /// Contains the base setup for a playable game
    /// </summary>
    /// <param name="playerOne">Player One Object</param>
    /// <param name="playerTwo">Player Two Object</param>
    public virtual void PlayGame(Player playerOne, Player playerTwo)
    {
        PlayerOne = playerOne;
        PlayerTwo = playerTwo;
        
        PlayerOne.ResetScore();
        PlayerTwo.ResetScore();
        
    }

    protected void EndGame()
    {
        Console.WriteLine("Game Over");
        
        if (PlayerOne.Score == PlayerTwo.Score)
        {
            Console.WriteLine($"Tie, both players scored {PlayerOne.Score} points");
        }
        else
        {
            Player winner;
            if (PlayerOne.Score > PlayerTwo.Score) winner = PlayerOne;
            else winner = PlayerTwo;
            
            Console.WriteLine($"The winner is {winner.Name} with {winner.Score} points");
        }
    }
}