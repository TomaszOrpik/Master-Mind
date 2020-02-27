using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GameLib;

namespace Master_Mind___Console_App
{
    class Program
    {
        //create object for easy save
        public static char[] _playerColors;
        public static char[] _playerNumbers;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to MasterMind!");
            Menu();
        }
        //main menu
        static void Menu()
        {
            //choose game mode
            while(Console.ReadKey().Key != ConsoleKey.Escape)
            {
                int colors = 6;
                int arr = 4;
                Console.WriteLine("Choose option by pressing the right key on keyboard, or press double times ESC to exit program:");
                if(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "state.save")) //save game exists
                    Console.WriteLine("1 - Continue last standard game player vs computer");
                else // save game doesn't exist
                    Console.WriteLine("1 - Standard game player vs computer");

                Console.WriteLine("2 - Standard game computer vs player");
                Console.WriteLine("3 - Cheating game computer vs player");
                Console.WriteLine("4 - Numbers game player vs computer");

                switch(Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("Set number of colors (6-8)");
                        try { colors = Convert.ToInt32(Console.ReadLine()); }
                        //string input exception
                        catch (FormatException)
                        {
                            Console.WriteLine("Incorrect Format");
                            Console.ReadKey();
                            Console.Clear();
                            Menu();
                        }
                            
                        Console.WriteLine("Set length of code (4-6)");
                        try { arr = Convert.ToInt32(Console.ReadLine()); }
                        //string input exception
                        catch (FormatException)
                        {
                            Console.WriteLine("Incorrect Format");
                            Console.ReadKey();
                            Console.Clear();
                            Menu();
                        }
                        PlayerGuess(colors, arr);
                        break;
                    case ConsoleKey.D2:
                        Console.WriteLine("Set number of colors (6-8)");
                        try { colors = Convert.ToInt32(Console.ReadLine()); }
                        //string input exception
                        catch (FormatException)
                        {
                            Console.WriteLine("Incorrect Format");
                            Console.ReadKey();
                            Console.Clear();
                            Menu();
                        }

                        Console.WriteLine("Set length of code (4-6)");
                        try { arr = Convert.ToInt32(Console.ReadLine()); }
                        //string input exception
                        catch (FormatException)
                        {
                            Console.WriteLine("Incorrect Format");
                            Console.ReadKey();
                            Console.Clear();
                            Menu();
                        }
                        ComputerGuess(colors, arr);
                        break;
                    case ConsoleKey.D3:
                        Console.WriteLine("Set number of colors (6-8)");
                        try { colors = Convert.ToInt32(Console.ReadLine()); }
                        //string input exception
                        catch (FormatException)
                        {
                            Console.WriteLine("Incorrect Format");
                            Console.ReadKey();
                            Console.Clear();
                            Menu();
                        }

                        Console.WriteLine("Set length of code (4-6)");
                        try { arr = Convert.ToInt32(Console.ReadLine()); }
                        //string input exception
                        catch (FormatException)
                        {
                            Console.WriteLine("Incorrect Format");
                            Console.ReadKey();
                            Console.Clear();
                            Menu();
                        }
                        ComputerGuess(colors, arr, false);
                        break;
                    case ConsoleKey.D4:
                        NumberGuess();
                        break;
                    default:
                        Menu();
                        break;
                }
            }
        }
        //play human vs computer
        public static void PlayerGuess(int colors, int arr)
        {
            List<int> NN = new List<int>();
            List<int> ND = new List<int>();
            Game game = new Game(colors, arr);
            int counter;

            _playerColors = new char[arr];

            int cursPosLeft = 0;
            int cursPosTop = 0;

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "state.save")) //save game exists
            {
                string tempS = "";
                FileStream fs = File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "state.save");
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0)
                    tempS = temp.GetString(b);
                counter = Convert.ToInt32(tempS[0]);
                fs.Close();
            }
            else // save game doesn't exist
                counter = 9;

            //loop for try counter
            while (counter > 0)
            {
                //print instructions
                Console.Clear();
                Console.WriteLine("Input s to save game and quit\n"); //add function
                Console.WriteLine($"Input {arr} colors choosen from below:");
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("r = red");
                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("y = yellow");
                Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("g = green");
                Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine("b = blue");
                Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine("m = magenta");
                Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("c = cyan");
                if(colors == 7)
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.WriteLine("v = violet");
                }
                if(colors == 8)
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.WriteLine("v = violet");
                    Console.ForegroundColor = ConsoleColor.DarkCyan; Console.WriteLine("a = azure");
                }

                Console.ForegroundColor = ConsoleColor.White;

                for (int i = 0; i < arr; i++)
                {
                    cursPosLeft = Console.CursorLeft;
                    cursPosTop = Console.CursorTop;
                    //remaining lives from new line

                    switch (Console.ReadLine().ToLower())
                    {
                        case "s":
                            //save game and quit
                            game.CreateSave();
                            Environment.Exit(0);
                            break;
                        case "r":
                            _playerColors[i] = 'r';
                            break;
                        case "y":
                            _playerColors[i] = 'y';
                            break;
                        case "g":
                            _playerColors[i] = 'g';
                            break;
                        case "b":
                            _playerColors[i] = 'b';
                            break;
                        case "m":
                            _playerColors[i] = 'm';
                            break;
                        case "c":
                            _playerColors[i] = 'c';
                            break;
                        case "v":
                            _playerColors[i] = 'v';
                            break;
                        case "a":
                            _playerColors[i] = 'a';
                            break;
                        default:
                            i--;
                            break;
                    };

                    Console.CursorTop = Console.WindowTop + Console.WindowHeight-3;
                    Console.WriteLine($"{counter-1} lives remaining");
                    Console.WriteLine($"{arr - i} letters to add");
                    // Restore previous position
                    Console.SetCursorPosition(cursPosLeft, cursPosTop);

                }

                (int, int) result = game.CheckForN(_playerColors);
                ND.Add(result.Item1);
                NN.Add(result.Item2);
                //print result
                Console.WriteLine("{0,-20} {1,5}\n", "ND", "NN");
                for (int k = 0; k < ND.Count; k++)
                    Console.WriteLine("{0,-20} {1,5}", ND[k], NN[k]);
                Console.ReadKey();
                //check if player won
                if(ND.Contains(arr))
                {
                    Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("You win!"); Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }

                counter--;
            }
            //result if players didn't won
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("You Lost!"); Console.ForegroundColor = ConsoleColor.White;

            Console.ReadKey();
            Console.Clear();

        }
        //play comp vs human
        public static void ComputerGuess(int colors, int arr, bool noCheating = true)
        {
            //define variables
            Game game = new Game(colors, arr);
            char[] compCode = new char[arr];
            char[] compTempCode = new char[arr];
            int counter = 9;
            bool check = false;
            int colorsCounter = 0;
            bool allColors = false;


            int cursPosLeft = 0;
            int cursPosTop = 0;
            //print instructions
            Console.Clear();
            Console.WriteLine($"Input {arr} colors choosen from below and press ENTER:");
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("r = red");
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("y = yellow");
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("g = green");
            Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine("b = blue");
            Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine("m = magenta");
            Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("c = cyan");
            if (colors == 7)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.WriteLine("v = violet");
            }
            if (colors == 8)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.WriteLine("v = violet");
                Console.ForegroundColor = ConsoleColor.DarkCyan; Console.WriteLine("a = azure");
            }
            Console.ForegroundColor = ConsoleColor.White;

            char[] playerCode = Console.ReadLine().ToCharArray();
            //check if player insert right number of chars
            if (playerCode.Length != arr)
            {
                Console.WriteLine("Incorrect input length");
                Console.ReadKey();
                ComputerGuess(colors, arr);
            }
            //check if arrays input are correct
            for (int i = 0; i < arr; i++)
            {
                if (playerCode[i] == 'r' || playerCode[i] == 'y' || playerCode[i] == 'g' || playerCode[i] == 'b' || playerCode[i] == 'm' || playerCode[i] == 'c' || playerCode[i] == 'v' || playerCode[i] == 'a')
                    check = true;
                else
                    check = false;
            }
            if(!check)
            {
                Console.WriteLine("Incorrect letters");
                Console.ReadKey();
                ComputerGuess(colors, arr);
            }


            while (counter > 0)
            {
                    cursPosLeft = Console.CursorLeft;
                    cursPosTop = Console.CursorTop;
                    //remaining lives from new line

                //print players code
                Console.WriteLine("Your code: ");
                ColorsPrinter(playerCode);
                Console.ForegroundColor = ConsoleColor.White;
                    
                Console.WriteLine("\nComputer guess: ");
                 switch (counter)
                 {
                    //check if colors exist in array
                            case 9:
                                if (colorsCounter == arr)
                                    break;
                        for (int k = 0; k < compTempCode.Length; k++)
                            compTempCode[k] = 'r';
                        for (int j=0; j<arr; j++)
                                {
                                    if (playerCode[j] == 'r')
                                    {
                                        compCode[colorsCounter] = 'r';
                                        colorsCounter++;
                                    }
                                }
                                break;
                            case 8:
                                if (colorsCounter == arr)
                                    break;
                        for (int k = 0; k < compTempCode.Length; k++)
                            compTempCode[k] = 'y';
                        for (int j = 0; j < arr; j++)
                                {
                                    if (playerCode[j] == 'y')
                                    {
                                        compCode[colorsCounter] = 'y';
                                        colorsCounter++;
                                    }
                                }
                                break;
                            case 7:
                                if (colorsCounter == arr)
                                    break;
                        for (int k = 0; k < compTempCode.Length; k++)
                            compTempCode[k] = 'g';
                        for (int j = 0; j < arr; j++)
                                {
                                    if (playerCode[j] == 'g')
                                    {
                                        compCode[colorsCounter] = 'g';
                                        colorsCounter++;
                                    }
                                }
                                break;
                            case 6:
                                if (colorsCounter == arr)
                                    break;
                        for (int k = 0; k < compTempCode.Length; k++)
                            compTempCode[k] = 'b';
                        for (int j = 0; j < arr; j++)
                                {
                                    if (playerCode[j] == 'b')
                                    {
                                        compCode[colorsCounter] = 'b';
                                        colorsCounter++;
                                    }
                                }
                                break;
                            case 5:
                                if (colorsCounter == arr)
                                    break;
                        for (int k = 0; k < compTempCode.Length; k++)
                            compTempCode[k] = 'm';
                        for (int j = 0; j < arr; j++)
                                {
                                    if (playerCode[j] == 'm')
                                    {
                                        compCode[colorsCounter] = 'm';
                                        colorsCounter++;
                                    }
                                }
                                break;
                            case 4:
                                if (colorsCounter == arr)
                                    break;
                        for (int k = 0; k < compTempCode.Length; k++)
                            compTempCode[k] = 'c';
                        for (int j = 0; j < arr; j++)
                                {
                                    if (playerCode[j] == 'c')
                                    {
                                        compCode[colorsCounter] = 'c';
                                        colorsCounter++;
                                    }
                                }
                                break;
                    case 3:
                        if (colorsCounter == arr)
                            break;
                        for (int k = 0; k < compTempCode.Length; k++)
                            compTempCode[k] = 'v';
                        for (int j = 0; j < arr; j++)
                        {
                            if (playerCode[j] == 'v')
                            {
                                compCode[colorsCounter] = 'v';
                                colorsCounter++;
                            }
                        }
                        break;
                    case 2:
                        if (colorsCounter == arr)
                            break;
                        for (int k = 0; k < compTempCode.Length; k++)
                            compTempCode[k] = 'a';
                        for (int j = 0; j < arr; j++)
                        {
                            if (playerCode[j] == 'a')
                            {
                                compCode[colorsCounter] = 'a';
                                colorsCounter++;
                            }
                        }
                        break;
                }
                //code to generate when colcounter = colorsLength
                if(allColors)
                {
                    //check right colors on different position
                    Random r = new Random();
                    compTempCode = compCode.OrderBy(x => r.Next()).ToArray();
                }

                if (colorsCounter == arr)
                    allColors = true;
                
                ColorsPrinter(compTempCode);
                Console.WriteLine();
                //player set number of ND and NN
                Console.WriteLine("Insert number of NDs:");
                int nd = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine("Insert number of NNs:");
                int nn = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                //check if ND+NN player didn't cheat in ND and NN length
                if(nd+nn>arr)
                {
                    Console.WriteLine("You made incorrect input");
                    Console.ReadKey();
                    ComputerGuess(colors, arr);
                }

                //check if player didn't cheat in input
                if(noCheating)
                {
                    (int, int) chck = Game.Compare(playerCode, compTempCode, arr);
                    (int, int) pchck;
                    pchck.Item1 = nd;
                    pchck.Item2 = nn;
                    if (chck != pchck)
                    {
                        Console.WriteLine("Incorrect NN and ND values");
                        (int, int) test = Game.Compare(playerCode, compTempCode, arr);
                        Console.WriteLine(test.Item1 + " " + test.Item2);
                        Console.ReadKey();
                        ComputerGuess(colors, arr);
                    }
                }
                
                //messsage when computer win
                if (nd == arr)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Computer win!"); Console.ForegroundColor = ConsoleColor.White;
                    counter = 0;
                    Console.ReadKey();
                    Console.Clear();
                    Menu();
                }

                Console.CursorTop = Console.WindowTop + Console.WindowHeight - 3;
                    Console.WriteLine($"{counter - 1} lives remaining");
                    // Restore previous position
                    Console.SetCursorPosition(cursPosLeft, cursPosTop);
                counter--;
            }
            //message when computer loose
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Computer lost!"); Console.ForegroundColor = ConsoleColor.White;

            Console.ReadKey();
            Console.Clear();
            Menu();
        }
        //Numbers in place of colors
        public static void NumberGuess()
        {
            //define variables
            List<int> NN = new List<int>();
            List<int> ND = new List<int>();
            GameNumbers game = new GameNumbers();
            int counter;

            _playerNumbers = new char[4];

            int cursPosLeft = 0;
            int cursPosTop = 0;

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "state.save")) //save game exists
            {
                string tempS = "";
                FileStream fs = File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "stateNum.save");
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0)
                    tempS = temp.GetString(b);
                counter = Convert.ToInt32(tempS[0]);
                fs.Close();
            }
            else // save game doesn't exist
                counter = 18;

            //loop for try counter
            while (counter > 0)
            {
                // print instructions
                Console.Clear();
                Console.WriteLine("Input s to save game and quit\n"); //add function
                Console.WriteLine("Input 4 numbers choosen from below:");
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("0");
                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("1");
                Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("2");
                Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine("3");
                Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine("4");
                Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("5");
                Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.WriteLine("6");
                Console.ForegroundColor = ConsoleColor.DarkCyan; Console.WriteLine("7");
                Console.ForegroundColor = ConsoleColor.DarkBlue; Console.WriteLine("8");
                Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine("9");
                

                Console.ForegroundColor = ConsoleColor.White;

                for (int i = 0; i < 4; i++)
                {
                    cursPosLeft = Console.CursorLeft;
                    cursPosTop = Console.CursorTop;
                    //remaining lives from new line

                    switch (Console.ReadLine().ToLower())
                    {
                        case "s":
                            //save game and quit
                            game.CreateSave();
                            Environment.Exit(0);
                            break;
                        case "0":
                            _playerNumbers[i] = '0';
                            break;
                        case "1":
                            _playerNumbers[i] = '1';
                            break;
                        case "2":
                            _playerNumbers[i] = '2';
                            break;
                        case "3":
                            _playerNumbers[i] = '3';
                            break;
                        case "4":
                            _playerNumbers[i] = '4';
                            break;
                        case "5":
                            _playerNumbers[i] = '5';
                            break;
                        case "6":
                            _playerNumbers[i] = '6';
                            break;
                        case "7":
                            _playerNumbers[i] = '7';
                            break;
                        case "8":
                            _playerNumbers[i] = '8';
                            break;
                        case "9":
                            _playerNumbers[i] = '9';
                            break;
                        default:
                            i--;
                            break;
                    };

                    Console.CursorTop = Console.WindowTop + Console.WindowHeight - 3;
                    Console.WriteLine($"{counter - 1} lives remaining");
                    Console.WriteLine($"{3 - i} letters to add");
                    // Restore previous position
                    Console.SetCursorPosition(cursPosLeft, cursPosTop);

                }

                //clear lists with answers when counter is bigger than screen size
                if(counter <= 9)
                {
                    ND.RemoveAt(0);
                    NN.RemoveAt(0);
                }

                (int, int) result = game.CheckForN(_playerNumbers);
                ND.Add(result.Item1);
                NN.Add(result.Item2);

                //print results
                Console.WriteLine("{0,-20} {1,5}\n", "ND", "NN");
                for (int k = 0; k < ND.Count; k++)
                    Console.WriteLine("{0,-20} {1,5}", ND[k], NN[k]);
                Console.ReadKey();
                //message when player won
                if (ND.Contains(4))
                {
                    Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("You win!"); Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Correct code: ");
                    NumbersPrinter(_playerNumbers);
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }

                counter--;
            }
            //result if player didn't won
            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("You Lost!"); Console.ForegroundColor = ConsoleColor.White;

            Console.ReadKey();
            Console.Clear();

        }

        //Change color of array
        public static void ColorsPrinter(char[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                switch (array[i])
                {
                    case 'r':
                        Console.ForegroundColor = ConsoleColor.Red; Console.Write("r");
                        break;
                    case 'y':
                        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("y");
                        break;
                    case 'g':
                        Console.ForegroundColor = ConsoleColor.Green; Console.Write("g");
                        break;
                    case 'b':
                        Console.ForegroundColor = ConsoleColor.Blue; Console.Write("b");
                        break;
                    case 'm':
                        Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("m");
                        break;
                    case 'c':
                        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("c");
                        break;
                    case 'v':
                        Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.Write("v");
                        break;
                    case 'a':
                        Console.ForegroundColor = ConsoleColor.DarkCyan; Console.Write("a");
                        break;
                    default:
                        break;
                };
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        //change color of numbers
        public static void NumbersPrinter(char[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                switch (array[i])
                {
                    case '0':
                        Console.ForegroundColor = ConsoleColor.Red; Console.Write("0");
                        break;
                    case '1':
                        Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("1");
                        break;
                    case '2':
                        Console.ForegroundColor = ConsoleColor.Green; Console.Write("2");
                        break;
                    case '3':
                        Console.ForegroundColor = ConsoleColor.Blue; Console.Write("3");
                        break;
                    case '4':
                        Console.ForegroundColor = ConsoleColor.Magenta; Console.Write("4");
                        break;
                    case '5':
                        Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("5");
                        break;
                    case '6':
                        Console.ForegroundColor = ConsoleColor.DarkMagenta; Console.Write("6");
                        break;
                    case '7':
                        Console.ForegroundColor = ConsoleColor.DarkCyan; Console.Write("7");
                        break;
                    case '8':
                        Console.ForegroundColor = ConsoleColor.DarkBlue; Console.Write("8");
                        break;
                    case '9':
                        Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write("9");
                        break;
                    default:
                        break;
                };
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
