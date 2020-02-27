using System;
using GameLib;

namespace MasterMindTest
{
    class Program
    {
        static void Main(string[] args)
        {

            Game game = new Game(6, 4);
            char[] test1 = { 'r', 'y', 'g', 'b' };
            char[] test2 = { 'r', 'y', 'g', 'c' };
            char[] test3 = { 'r', 'y', 'c', 'c' };
            char[] test4 = { 'r', 'c', 'c', 'c' };
            char[] test5 = { 'c', 'c', 'c', 'c' };
            char[] test6 = { 'c', 'r', 'c', 'c' };
            char[] test7 = { 'y', 'r', 'b', 'g' };
            char[] test8 = { 'r', 'y', 'y', 'r' };
            char[] test9 = { 'r', 'g', 'y', 'b' };

            game._code = test1;
            Console.WriteLine(game._code);
            (int, int) result1 = game.CheckForN(test1);
            Console.WriteLine(result1.Item1 + " " + result1.Item2); //4 0
            (int, int) result2 = game.CheckForN(test2);
            Console.WriteLine(result2.Item1 + " " + result2.Item2); //3 0
            (int, int) result3 = game.CheckForN(test3);
            Console.WriteLine(result3.Item1 + " " + result3.Item2); //2 0
            (int, int) result4 = game.CheckForN(test4);
            Console.WriteLine(result4.Item1 + " " + result4.Item2); //1 0
            (int, int) result5 = game.CheckForN(test5);
            Console.WriteLine(result5.Item1 + " " + result5.Item2); //0 0
            (int, int) result6 = game.CheckForN(test6);
            Console.WriteLine(result6.Item1 + " " + result6.Item2); //0 1
            (int, int) result7 = game.CheckForN(test7);
            Console.WriteLine(result7.Item1 + " " + result7.Item2); //0 4
            (int, int) result8 = game.CheckForN(test8);
            Console.WriteLine(result8.Item1 + " " + result8.Item2); //2 0
            (int, int) result9 = game.CheckForN(test9);
            Console.WriteLine(result9.Item1 + " " + result9.Item2); //2 2

            //check for compare
            Console.WriteLine();
            (int, int) result11 = Game.Compare(test1, test1, 4);
            Console.WriteLine(result11.Item1 + " " + result11.Item2); //4 0
            (int, int) result12 = Game.Compare(test1, test2, 4);
            Console.WriteLine(result12.Item1 + " " + result12.Item2); //3 0
            (int, int) result13 = Game.Compare(test1, test3, 4);
            Console.WriteLine(result13.Item1 + " " + result13.Item2); //2 0
            (int, int) result14 = Game.Compare(test1, test4, 4);
            Console.WriteLine(result14.Item1 + " " + result14.Item2); //1 0
            (int, int) result15 = Game.Compare(test1, test5, 4);
            Console.WriteLine(result15.Item1 + " " + result15.Item2); //0 0
            (int, int) result16 = Game.Compare(test1, test6, 4);
            Console.WriteLine(result16.Item1 + " " + result16.Item2); //0 1
            (int, int) result17 = Game.Compare(test1, test7, 4);
            Console.WriteLine(result17.Item1 + " " + result17.Item2); //0 4
            (int, int) result18 = Game.Compare(test1, test8, 4);
            Console.WriteLine(result18.Item1 + " " + result18.Item2); //2 0
            (int, int) result19 = Game.Compare(test1, test9, 4);
            Console.WriteLine(result19.Item1 + " " + result19.Item2); //2 2
            char[] test201 = { 'r', 'g', 'b', 'm' };
            char[] test202 = {'b', 'g', 'r', 'g'};
            (int, int) result20 = Game.Compare(test201, test202, 4);
            Console.WriteLine(result20.Item1 + " " + result20.Item2);
            game._code = test201;
            (int, int) result21 = game.CheckForN(test202);
            Console.WriteLine(result21.Item1 + " " + result21.Item2);
            /*
            game.CreateSave();
            
            Game game = new Game();
            Console.WriteLine(game._code);
            */


        }
    }
}
