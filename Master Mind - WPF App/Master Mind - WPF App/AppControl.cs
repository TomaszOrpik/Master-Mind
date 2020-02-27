using GameLib;
using System;
using System.IO;
using System.Text;

namespace Master_Mind___WPF_App
{
    class AppControl
    {
        //define variables
        AppView _view;
        WindowGame _windowGame;
        Game _game;
        GameNumbers _gameNumb;
        int _colors;
        public int _length;
        int _counter;
        private static int _globalCounter;

        //mode for 1st button
        public void PlayerGuess(int colors, int length)
        {
            _colors = colors;
            _length = length;
            _game = new Game(_colors, _length);
            _windowGame = new WindowGame(_game, _length, _colors, 1, false);
            _view = new AppView(_windowGame); 

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "state.save")) //save game exists
            {
                string tempS = "";
                FileStream fs = File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "state.save");
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0)
                    tempS = temp.GetString(b);
                _counter = Convert.ToInt32(tempS[0]);
                fs.Close();
            }
            else // save game doesn't exist
                _counter = 9;
            _view.Start(colors, length);
            MyConstr();
            _windowGame.Show();

        }
        //mode for 2nd and 3rd button
        public void ComputerGuess(int colors, int length, bool cheat)
        {
            _colors = colors;
            _length = length;
            _game = new Game(_colors, _length);
            _windowGame = new WindowGame(_game, _length,_colors, 2, cheat);
            _view = new AppView(_windowGame);

            _counter = 9;
            _view.StartVsComp(colors, length);
            MyConstr();
            _windowGame.Show();

        }
        //mode for 4th button
        public void NumberGuess()
        {
            _gameNumb = new GameNumbers();
            _windowGame = new WindowGame(_gameNumb);
            _view = new AppView(_windowGame);

            _counter = 9;
            _view.StartNumbers();
            _windowGame.Show();
        }

        public Game GetGame()
        {
            return this._game;
        }
        //constructor for global counter
        private void MyConstr()
        {
            _globalCounter = _counter;
        }

        public int GetCounter()
        {
            return _globalCounter;
        }
        //check for player tries
        public (int, int) PlayerTry(char[] playerArr)
        {
            return _game.CheckForN(playerArr);
        }

        public int Counting()
        {
            return _counter--;
        }
    }
}
