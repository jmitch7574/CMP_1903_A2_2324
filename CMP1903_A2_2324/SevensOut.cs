namespace CMP1903_A2_2324;

public class SevensOut : Game
{
    public override void PlayGame(Player playerOne, Player playerTwo)
    {
        base.PlayGame(playerOne, playerTwo);

        bool playing = true;
        int turn = 0;

        Die[] die = new[] { new Die(), new Die() };

        while (true)
        {
            Console.WriteLine();
            
            Player player;
            if (turn % 2 == 0) player = PlayerOne;
            else player = PlayerTwo;
            
            player.AwaitTurn();

            die[0].Roll();
            die[1].Roll();
            
            Console.WriteLine(die[0]);
            Console.WriteLine(die[1]);
            
            int dieTotal = die[0].DieValue + die[1].DieValue;

            if (dieTotal == 7) break;
            
            if (die[0].DieValue == die[1].DieValue)
            {
                Console.WriteLine("Doubles! Added score will be doubled");
                player.AddScore(dieTotal * 2);
            }
            else player.AddScore(dieTotal);

            turn++;
        }
        
        Console.WriteLine("Sevens Out!!!");
        EndGame();
    }
}