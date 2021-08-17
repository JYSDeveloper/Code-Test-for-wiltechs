using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeTest
{
    public class PokersCollection
    {
        public List<int> Items { get; set; }

        public PokersCollection()
        {
            Items = new List<int>() { 3, 7, 5 };
        }

        public void SmartPick()
        {
            //If only one line have pokers, just leave one poker in this line to the opponent
            if (Items.Count == 1)
            {
                Console.WriteLine($"Computer pick {Items[0] - 1}");
                Items[0] = 1;
                return;
            }

            //If two lines have pokers
            //One line is 1, just pick all of the another line's pokers
            //Otherwise, make the number of cards on both lines equal.
            if (Items.Count == 2)
            {
                var left = Items[0];
                var right = Items[1];
                if(left == 1)
                {
                    Console.WriteLine($"Computer pick {right}");
                    Items.RemoveAt(1);
                }else if(right == 1)
                {
                    Console.WriteLine($"Computer pick {left}");
                    Items.RemoveAt(0);
                }
                else
                {
                    Items[0] = left > right ? right : left;
                    Items[1] = left > right ? right : left;
                    Console.WriteLine($"Computer pick {Math.Abs(left - right)}");
                }
                return;
            }

            //If have three lines, it's very complicated.
            if (Items.Count == 3)
            {
                //First, if two line's pokers with save count, such as X,2,2, just leave 2,2 to the opponent will win this game
                var index = RemoveNotEqualOne();
                if (index != -1)
                {
                    Console.WriteLine($"Computer pick {Items[index]}");
                    Items.RemoveAt(index);
                    return;
                }
                var max = FindMaxWithIndex();

                int threshold = Items.Where(x => x <= 3).Count();
                //If all line's pokers number more than 3, just leave one line's poker to 1.
                if (threshold == 0)
                {
                    Console.WriteLine($"Computer pick {max.Item2 - 1}");
                    Items[max.Item1] = 1;
                    return;
                }

                if(threshold == 2)
                {
                    var tag = 3;
                    while (Items.Any(x => x == tag))
                        tag--;
                    Console.WriteLine($"Computer pick {max.Item2 - tag}");
                    Items[max.Item1] = tag;
                    return;
                }

                //Only have one part of poker less or equal to 3, in order to let the opponent set another poker less than 3,
                //set the value to 4 or 5. Avoid to make duplicate number, so use contains method, set value to 4 or 5.
                if (Items.Contains(4) && max.Item2 != 5)
                {
                    Console.WriteLine($"Computer pick {max.Item2 - 5}");
                    Items[max.Item1] = 5;
                    return;
                }
                
                if (Items.Contains(5) && max.Item2 != 5)
                {
                    Console.WriteLine($"Computer pick {max.Item2 - 4}");
                    Items[max.Item1] = 4;
                    return;
                }

                int i = 1;
                while (i < max.Item2)
                {
                    var tmp = max.Item2 - i;
                    //Make sure if take some pokers from one line, Remaining values not equal with other lines.
                    if (!Items.Any(x => tmp == x))
                    {
                        Console.WriteLine($"Computer pick {i}");
                        Items[max.Item1] = tmp;
                        break;
                    }
                    i++;
                }
                //All Remaining values will equal with other lines, just remove this line.Only happened on the another two line is 1 and 2
                if (max.Item2 == i)
                {
                    Console.WriteLine($"Computer pick {i}");
                    Items.RemoveAt(max.Item1);
                }

            }
        }

        private (int, int) FindMaxWithIndex()
        {
            int maxValue= -1;
            int maxIndex = -1;
            for (int i = 0; i < Items.Count; i++)
            {
                if(Items[i] > maxValue)
                {
                    maxValue = Items[i];
                    maxIndex = i;
                }
            }
            return (maxIndex, maxValue);
        }

        private int RemoveNotEqualOne()
        {
            if (Items[0] == Items[1])
                return 2;
            if (Items[0] == Items[2])
                return 1;
            if (Items[1] == Items[2])
                return 0;
            return -1;
        }

        public void ShowSelf()
        {
            Console.WriteLine("Current Status:");
            for (int i = 0; i <Items.Count; i++)
            {
                Console.WriteLine($"[{i}] -> {Items[i]}");
            }
        }

 
        public bool GaveOver()
        {
            Items = Items.Where(x => x != 0).ToList();
            if (Items.Count == 1 && Items[0] == 1)
                return true;
            return false;
        }
    }
}
