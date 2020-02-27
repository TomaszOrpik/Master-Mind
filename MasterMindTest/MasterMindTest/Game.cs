using System;
using System.IO;
using System.Linq;
using System.Text;

namespace GameLib
{
    public class Game
    {
        public char[] _code;
        private int _counter = 0;
        private string path = AppDomain.CurrentDomain.BaseDirectory + "state.save";

        private int _colors;
        private int _arr;

        public Game(int colors, int arr)
        {
            //check for correct colors and tries input

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

            _colors = colors;
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
            for (int i = 0; i < _arr; i++)
            {
                if (temp[i] == tempAttempt[i])
                {
                    nd++;
                    tempClone = tempClone.Where(x => (x != temp[i])).ToArray();
                }
            }
            for (int i = 0; i < _arr; i++)
            {
                if (tempClone.Contains(tempAttempt[i]))
                    nn++;
            }
            return (nd, nn);
        }
        //methods for create .save file
        public void CreateSave()
        {
            using (FileStream fs = File.Create(path))
            {
                AddText(fs, (Convert.ToString(_counter)));
                foreach (char el in _code)
                    AddText(fs, (Convert.ToString(el)));

            }
        }
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
                    8 => code[i] = 'p',
                    _ => throw new ArgumentException("code didn't generate correctly"),
                });
            }
            return code;
        }

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
}

