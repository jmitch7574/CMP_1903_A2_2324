namespace CMP1903_A2_2324;
using System.Threading;

public class ComputerPlayer : Player
{
    
    public ComputerPlayer()
    {
        IsComputer = true;
        Name = "Computer";
    }
    public override void AwaitTurn()
    {
        Console.WriteLine($"{Name} is now taking their turn");
        Thread.Sleep(3000);
    }
}