using GameLib;
using System;
using System.Windows;
using System.Windows.Media;

namespace Master_Mind___WPF_App
{
    /// <summary>
    /// Logika interakcji dla klasy WindowGame.xaml
    /// </summary>
    public partial class WindowGame : Window
    {
        //initialize variables
        public int _lives;
        AppView _view;
        AppControl _control;
        Game _game;
        GameNumbers _gameNumb;
        int _length;
        int _colors;
        int _mode;
        char[] _playerCode;
        bool _cheatMode;

        public WindowGame(Game game, int length, int colors, int mode, bool cheat)
        {
            //main window constructor
            InitializeComponent();
            _control = new AppControl();
            _lives = _control.GetCounter();
            _view = new AppView();
            _game = game;
            _length = length;
            _colors = colors;
            _mode = mode;
            _cheatMode = cheat;
        }
        public WindowGame(GameNumbers gameNumb)
        {
            //main window alternative constructor
            InitializeComponent();
            _gameNumb = gameNumb;
            _mode = 4;
            _length = 4;
            _colors = 10;
            _control = new AppControl();
            _view = new AppView();
            _lives = _control.GetCounter();
        }

        private void ND_NN_Confirm_Click(object sender, RoutedEventArgs e) //check if ND and NN is correct by MB //edit remaining lives (one below) // print result if win or lost
        {
            char[] compTry = Game.CodeGenerate(_length, _colors);
            //write computers code
            if(_length == 4)
                CompInput.Content = $"Computer Guess: {compTry[0].ToString()},{compTry[1].ToString()},{compTry[2].ToString()},{compTry[3].ToString()}";
            if (_length == 5)
                CompInput.Content = $"Computer Guess: {compTry[0].ToString()},{compTry[1].ToString()},{compTry[2].ToString()},{compTry[3].ToString()},{compTry[4].ToString()}";
            if (_length == 6)
                CompInput.Content = $"Computer Guess: {compTry[0].ToString()},{compTry[1].ToString()},{compTry[2].ToString()},{compTry[3].ToString()},{compTry[4].ToString()},{compTry[5].ToString()}";

            (int, int) result = Game.Compare(_playerCode, compTry, _length);
            //blocking cheating
            if ((result.Item1 != Convert.ToInt32(NDbOX.Text.ToString()) || result.Item2 != Convert.ToInt32(NNsBox.Text.ToString())) && _cheatMode == false)
            {
                MessageBoxResult mb = MessageBox.Show("Incorrect ND or NN, please don't cheat!");
                return;
            }
            else
            {
                ResultsListBox.Items.Add(_view.CreateListBoxItem(result.Item1, result.Item2, _playerCode, _length));
                LivesLabel.Content = _view.DisplayLives(_lives);
                _lives--;
                //win/lose result
                if (result.Item1 == 4)
                {
                    ResultLabel.Foreground = Brushes.Red;
                    ResultLabel.Content = "Computer Win!";
                    Confirm.IsEnabled = false;
                }
                if (_lives == 0)
                {
                    ResultLabel.Foreground = Brushes.Green;
                    ResultLabel.Content = "Computer Lost!";
                    Confirm.IsEnabled = false;
                }
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {

                char[] playerCodeArr = new char[_length];
            //read char from ComboBox
                playerCodeArr[0] = Convert.ToChar(Color1.SelectedItem.ToString().Split(": ")[1].ToLower());
                playerCodeArr[1] = Convert.ToChar(Color2.SelectedItem.ToString().Split(": ")[1].ToLower());
                playerCodeArr[2] = Convert.ToChar(Color3.SelectedItem.ToString().Split(": ")[1].ToLower());
                playerCodeArr[3] = Convert.ToChar(Color4.SelectedItem.ToString().Split(": ")[1].ToLower());
                if (_length == 5)
                    playerCodeArr[4] = Convert.ToChar(Color5.SelectedItem.ToString().Split(": ")[1].ToLower());
                if (_length == 6)
                {
                    playerCodeArr[4] = Convert.ToChar(Color5.SelectedItem.ToString().Split(": ")[1].ToLower());
                    playerCodeArr[5] = Convert.ToChar(Color6.SelectedItem.ToString().Split(": ")[1].ToLower());
                }
            if (_mode == 1)
            {
                //check for ND and NN
                (int, int) result = _game.CheckForN(playerCodeArr);
                //add result to list of tries
                ResultsListBox.Items.Add(_view.CreateListBoxItem(result.Item1, result.Item2, playerCodeArr, _length));
                LivesLabel.Content = _view.DisplayLives(_lives);
                _lives--;
                //win/lost info
                if (result.Item1 == 4)
                {
                    ResultLabel.Foreground = Brushes.Green;
                    ResultLabel.Content = "You Win!";
                    Confirm.IsEnabled = false;
                }
                if (_lives == 0)
                {
                    ResultLabel.Foreground = Brushes.Red;
                    ResultLabel.Content = "You Lost!";
                    Confirm.IsEnabled = false;
                }
            }
            //blocking color input after player selecting code
                if (_mode == 2)
                {
                    _playerCode = playerCodeArr;
                    Color1.IsEnabled = false;
                    Color2.IsEnabled = false;
                    Color3.IsEnabled = false;
                    Color4.IsEnabled = false;
                    Color5.IsEnabled = false;
                    Color6.IsEnabled = false;
                    Confirm.IsEnabled = false;
                }
            if(_mode == 4)
            {
                ///code in numbers mode
                (int, int) result = _gameNumb.CheckForN(playerCodeArr);

                ResultsListBox.Items.Add(_view.CreateListBoxItem(result.Item1, result.Item2, playerCodeArr, _length));
                LivesLabel.Content = _view.DisplayLives(_lives);
                _lives--;
                //lost/win info
                if (result.Item1 == 4)
                {
                    ResultLabel.Foreground = Brushes.Green;
                    ResultLabel.Content = "You Win!";
                    Confirm.IsEnabled = false;
                }
                if (_lives == 0)
                {
                    ResultLabel.Foreground = Brushes.Red;
                    ResultLabel.Content = "You Lost!";
                    Confirm.IsEnabled = false;
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //save current code on window closed event
            if(_mode != 4)
            _game.CreateSave();
        }
    }
}
