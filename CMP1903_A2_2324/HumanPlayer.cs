namespace CMP1903_A2_2324;

public class HumanPlayer : Player
{
    public HumanPlayer(string name)
    {
        Name = name;
        IsComputer = false;
    }
    
    public override void AwaitTurn()
    {
        Console.WriteLine($"{Name}, please press any key to take your turn");
        Console.ReadKey();
    }
}