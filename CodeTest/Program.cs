using System;

namespace CodeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var pokers = new PokersCollection();

            pokers.ShowSelf();
            while (!pokers.GaveOver())
            {
                pokers.SmartPick();
                pokers.ShowSelf();
                if (pokers.GaveOver())
                {
                    Console.WriteLine("User lose this game");
                    return;
                }
                bool flag = false;
                while (!flag)
                {
                    Console.WriteLine("Please enter which line you want pick(start from 0)");
                    var i = Console.ReadLine();
                    Console.WriteLine("Please enter how many pockers you want pick");
                    var j = Console.ReadLine();
                    if(int.TryParse(i, out var line) && int.TryParse(j, out var numbers) && pokers.Items.Count > line && pokers.Items[line] >= numbers)
                    {
                        Console.WriteLine($"User pick {numbers} pockers");
                        pokers.Items[line] = pokers.Items[line] - numbers;
                        pokers.ShowSelf();
                        flag = true;
                    }
                    else
                    {
                        Console.WriteLine("Please re-enter the valid line & numbers again");
                    }
                }
            }
            Console.WriteLine("Computer lose this game"); //impossible


        }
    }
}
