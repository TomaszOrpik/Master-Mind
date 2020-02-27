using System;
using System.IO;
using System.Linq;
using System.Text;

namespace GameLib
{
    public class Game
    {
        //define variables
        private char[] _code;
        private int _counter = 0;
        private string path = AppDomain.CurrentDomain.BaseDirectory + "state.save";

        private int _arr;

        public Game(int colors, int arr)
        {
            //check for correct colors and tries input
            if (colors < 6 && colors > 8)
                throw new Exception("Incorrect colors input");
            if (arr < 4 && arr > 6)
                throw new Exception("Incorrect length input");
            if (arr >= colors)
                throw new Exception("Number of colors must smaller than length");

            //load stats from .save file
            if (File.Exists(path))
            {
                //code from file + number of attempts from file
                string tempS = "";
                FileStream fs = File.OpenRead(path);
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0)
                    tempS = temp.GetString(b);
                _counter = Convert.ToInt32(tempS[0]);
                _code = tempS.ToCharArray(1, arr);
                fs.Close();
                File.Delete(path);
            }
            //create new
            else
            {
                _code = CodeGenerate(arr, colors);
            }

            _arr = arr;
        }
        //check if color and position is correct/color or position is correct
        public (int, int) CheckForN(char[] attempt)
        {
            int nd = 0;
            int nn = 0;
            char[] temp = _code;
            char[] tempAttempt = attempt;
            char[] tempClone = _code;
            //check for ND
            for (int i = 0; i < _arr; i++)
            {
                if (temp[i] == tempAttempt[i])
                {
                    nd++;
                    tempClone = tempClone.Where(x => (x != temp[i])).ToArray();
                }
            }
            //check for NN
            for (int i = 0; i < _arr; i++)
            {
                if (tempClone.Contains(tempAttempt[i]))
                    nn++;
            }
            return (nd, nn);
        }
        //method for create .save file
        public void CreateSave()
        {
            using (FileStream fs = File.Create(path))
            {
                AddText(fs, (Convert.ToString(_counter)));
                foreach (char el in _code)
                    AddText(fs, (Convert.ToString(el)));

            }
        }
        //insert data into .save file
        public void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
        //method to generate new code
        public static char[] CodeGenerate(int arr, int colors)
        {
            char[] code = new char[arr];
            int num;
            Random random = new Random();
            for (int i = 0; i < arr; i++)
            {
                num = random.Next(1, colors);
                _ = (num switch
                {
                    1 => code[i] = 'r',
                    2 => code[i] = 'y',
                    3 => code[i] = 'g',
                    4 => code[i] = 'b',
                    5 => code[i] = 'm',
                    6 => code[i] = 'c',
                    7 => code[i] = 'v',
                    8 => code[i] = 'a',
                    _ => throw new ArgumentException("code didn't generate correctly"),
                });
            }
            return code;
        }
        //method to compare to char arrays
        public static (int, int) Compare(char[] arr1, char[] arr2, int arr)
        {
            int nd = 0;
            int nn = 0;
            char[] arr1Clone = arr1;
            for (int i = 0; i < arr; i++)
            {
                if (arr1[i] == arr2[i])
                {
                    nd++;
                    arr1Clone = arr1Clone.Where(x => (x != arr1[i])).ToArray();
                }
            }
            for (int i = 0; i < arr; i++)
            {
                if (arr1Clone.Contains(arr2[i]))
                    nn++;
            }
            return (nd, nn);
        }
    }

    public class GameNumbers
    {
        //define variables
        private char[] _code;
        private int _counter = 0;
        private string path = AppDomain.CurrentDomain.BaseDirectory + "stateNum.save";

        public GameNumbers()
        {
            //load stats from .save file
            if (File.Exists(path))
            {
                //code from file + number of attempts from file
                string tempS = "";
                FileStream fs = File.OpenRead(path);
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0)
                    tempS = temp.GetString(b);
                _counter = Convert.ToInt32(tempS[0]);
                _code = tempS.ToCharArray(1, 4);
                fs.Close();
                File.Delete(path);
            }
            //create new
            else
            {
                _code = CodeGenerate();
            }

        }
        //check if color and position is correct/color or position is correct
        public (int, int) CheckForN(char[] attempt)
        {
            int nd = 0;
            int nn = 0;
            char[] temp = _code;
            char[] tempAttempt = attempt;
            char[] tempClone = _code;
            //check for ND
            for (int i = 0; i < 4; i++)
            {
                if (temp[i] == tempAttempt[i])
                {
                    nd++;
                    tempClone = tempClone.Where(x => (x != temp[i])).ToArray();
                }
            }
            //check for NN
            for (int i = 0; i < 4; i++)
            {
                if (tempClone.Contains(tempAttempt[i]))
                    nn++;
            }
            return (nd, nn);
        }
        //method for create .save file
        public void CreateSave()
        {
            using (FileStream fs = File.Create(path))
            {
                AddText(fs, (Convert.ToString(_counter)));
                foreach (char el in _code)
                    AddText(fs, (Convert.ToString(el)));

            }
        }
        //method to insert data into .save file
        public void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
        //method to generate new code
        public static char[] CodeGenerate()
        {
            char[] code = new char[4];
            int num;
            Random random = new Random();
            for (int i = 0; i < 4; i++)
            {
                num = random.Next(1, 10);
                _ = (num switch
                {
                    1 => code[i] = '1',
                    2 => code[i] = '2',
                    3 => code[i] = '3',
                    4 => code[i] = '4',
                    5 => code[i] = '5',
                    6 => code[i] = '6',
                    7 => code[i] = '7',
                    8 => code[i] = '8',
                    9 => code[i] = '9',
                    10 => code[i] = '0',
                    _ => throw new ArgumentException("code didn't generate correctly"),
                });
            }
            return code;
        }
        //method to compare 2 char arrays
        public static (int, int) Compare(char[] arr1, char[] arr2)
        {
            int nd = 0;
            int nn = 0;
            char[] arr1Clone = arr1;
            for (int i = 0; i < 4; i++)
            {
                if (arr1[i] == arr2[i])
                {
                    nd++;
                    arr1Clone = arr1Clone.Where(x => (x != arr1[i])).ToArray();
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (arr1Clone.Contains(arr2[i]))
                    nn++;
            }
            return (nd, nn);
        }
    }
}

