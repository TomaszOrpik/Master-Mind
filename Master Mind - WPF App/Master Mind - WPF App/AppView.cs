using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Master_Mind___WPF_App
{
    class AppView
    {
        //define variables
        WindowGame _gameWindow;
        int _numberOfColors;
        int _codeLength;
        public List<ComboBox> _colorChoose = new List<ComboBox>();
        //default constructor
        public AppView(WindowGame wg)
        {
            _gameWindow = wg;
        }
        //alternative (empty) constructor
        public AppView() { }


        public void Start(int colors, int length) 
        {
            GetValues(colors, length);
            ClearForm();
            //DisplayNN_ND();
            DisplayColorBox();
            CreateColors();
        }

        public void StartVsComp(int colors, int length)
        {
            //create method to start new game versus computer
            GetValues(colors, length);
            ClearForm();
            DisplayNN_ND();
            DisplayColorBox();
            CreateColors();
        }
        public void StartNumbers()
        {
            //generate numbers view
            GetValues(10, 4);
            ClearForm();
            DisplayColorBox();
            CreateNumbers();
        }

        public static void ModeName()
        {
            MainWindow window = Application.Current.MainWindow as MainWindow;

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "state.save")) //save game exists
                window.Mode1btn.Content = "Continue last game player vs computer";
            else // save game doesn't exist
                window.Mode1btn.Content = "Standard game player vs computer";
        }

        public void GetValues(int colors, int length)
        {
            //take values from control
            _numberOfColors = colors;
            _codeLength = length;

            _colorChoose.Add(_gameWindow.Color1);
            _colorChoose.Add(_gameWindow.Color2);
            _colorChoose.Add(_gameWindow.Color3);
            _colorChoose.Add(_gameWindow.Color4);
            _colorChoose.Add(_gameWindow.Color5);
            _colorChoose.Add(_gameWindow.Color6);
        }

        public void ClearForm()
        {
            //set form to default values
            _gameWindow.NNsBox.Items.Clear();
            _gameWindow.NDbOX.Items.Clear();
            foreach (ComboBox cb in _colorChoose)
                cb.Items.Clear();

            _gameWindow.ResultLabel.Content = "";
            _gameWindow.LivesLabel.Content = "Lives";
            _gameWindow.ResultsListBox.Items.Clear();
            //make all buttons invisible
            _gameWindow.Confirm.Visibility = Visibility.Hidden;
            _gameWindow.ND_NN_Confirm.Visibility = Visibility.Hidden;
            _gameWindow.NNsBox.Visibility = Visibility.Hidden;
            _gameWindow.NDbOX.Visibility = Visibility.Hidden;
            _gameWindow.NDCounter.Visibility = Visibility.Hidden;
            _gameWindow.NNCounter.Visibility = Visibility.Hidden;
        }
        
        public void DisplayNN_ND()
        {
            //make NN ND and button visible
            for (int i = 0; i <= _codeLength; i++)
            {
                ComboBoxItem cbin = new ComboBoxItem();
                cbin.Name = "ND" + i + 1;
                cbin.Content = i;
                _gameWindow.NNsBox.Items.Add(cbin);
            }
            for (int i = 0; i <= _codeLength; i++)
            {
                ComboBoxItem cbid = new ComboBoxItem();
                cbid.Name = "NN" + i + 1;
                cbid.Content = i;
                _gameWindow.NDbOX.Items.Add(cbid);
            }
            _gameWindow.ND_NN_Confirm.Visibility = Visibility.Visible;
            _gameWindow.NNsBox.Visibility = Visibility.Visible;
            _gameWindow.NDbOX.Visibility = Visibility.Visible;
            _gameWindow.NDCounter.Visibility = Visibility.Visible;
            _gameWindow.NNCounter.Visibility = Visibility.Visible;
        }
        //display number of active ComboBox(based on choosen length)
        public void DisplayColorBox()
        {
            for (int i = 5; i >= _codeLength; i--)
                _colorChoose[i].Visibility = Visibility.Hidden;
            //make button visible
            _gameWindow.Confirm.Visibility = Visibility.Visible;
        }
        //insert colors into choose ComboBox
        public void CreateColors()
        {
            for(int j = 0; j< _codeLength; j++)
            {
                List<ComboBoxItem> colList = new List<ComboBoxItem>();
                for(int i =1; i<=_numberOfColors; i++)
                {
                    switch(i)
                    {
                        case 1: ///index out of range exception
                            ComboBoxItem cbi1 = CreateColor('R', j);
                            cbi1.Foreground = Brushes.Red;
                            _colorChoose[j].Items.Add(cbi1);
                            break;
                        case 2:
                            ComboBoxItem cbi2 = CreateColor('Y', j);
                            cbi2.Foreground = Brushes.Yellow;
                            _colorChoose[j].Items.Add(cbi2);
                            break;
                        case 3:
                            ComboBoxItem cbi3 = CreateColor('G', j);
                            cbi3.Foreground = Brushes.Green;
                            _colorChoose[j].Items.Add(cbi3);
                            break;
                        case 4:
                            ComboBoxItem cbi4 = CreateColor('B', j);
                            cbi4.Foreground = Brushes.Blue;
                            _colorChoose[j].Items.Add(cbi4);
                            break;
                        case 5:
                            ComboBoxItem cbi5 = CreateColor('M', j);
                            cbi5.Foreground = Brushes.Magenta;
                            _colorChoose[j].Items.Add(cbi5);
                            break;
                        case 6:
                            ComboBoxItem cbi6 = CreateColor('C', j);
                            cbi6.Foreground = Brushes.Cyan;
                            _colorChoose[j].Items.Add(cbi6);
                            break;
                        case 7:
                            ComboBoxItem cbi7 = CreateColor('V', j);
                            cbi7.Foreground = Brushes.Violet;
                            _colorChoose[j].Items.Add(cbi7);
                            break;
                        case 8:
                            ComboBoxItem cbi8 = CreateColor('A', j);
                            cbi8.Foreground = Brushes.Azure;
                            _colorChoose[j].Items.Add(cbi8);
                            break;
                    }
                }

            }
        }
        //insert numbers into choose ComboBox
        public void CreateNumbers()
        {
            for (int j = 0; j < 4; j++)
            {
                List<ComboBoxItem> colList = new List<ComboBoxItem>();
                for (int i = 0; i <= 9; i++)
                {
                    switch (i)
                    {
                        case 0:
                            ComboBoxItem cb0 = CreateNumber(i);
                            _colorChoose[j].Items.Add(cb0);
                            break;
                        case 1:
                            ComboBoxItem cb1 = CreateNumber(i);
                            _colorChoose[j].Items.Add(cb1);
                            break;
                        case 2:
                            ComboBoxItem cb2 = CreateNumber(i);
                            _colorChoose[j].Items.Add(cb2);
                            break;
                        case 3:
                            ComboBoxItem cb3 = CreateNumber(i);
                            _colorChoose[j].Items.Add(cb3);
                            break;
                        case 4:
                            ComboBoxItem cb4 = CreateNumber(i);
                            _colorChoose[j].Items.Add(cb4);
                            break;
                        case 5:
                            ComboBoxItem cb5 = CreateNumber(i);
                            _colorChoose[j].Items.Add(cb5);
                            break;
                        case 6:
                            ComboBoxItem cb6 = CreateNumber(i);
                            _colorChoose[j].Items.Add(cb6);
                            break;
                        case 7:
                            ComboBoxItem cb7 = CreateNumber(i);
                            _colorChoose[j].Items.Add(cb7);
                            break;
                        case 8:
                            ComboBoxItem cb8 = CreateNumber(i);
                            _colorChoose[j].Items.Add(cb8);
                            break;
                        case 9:
                            ComboBoxItem cb9 = CreateNumber(i);
                            _colorChoose[j].Items.Add(cb9);
                            break;
                    }
                }

            }
        }

        public ComboBoxItem CreateColor(char i, int j)
        {
            ComboBoxItem cbi = new ComboBoxItem();
            cbi.Name = "Col" + j + i;
            cbi.Content = i;
            return cbi;
        }

        public ComboBoxItem CreateNumber(int i)
        {
            ComboBoxItem cbi = new ComboBoxItem();
            cbi.Name = "Col" + i;
            cbi.Content = i;
            return cbi;
        }

        public string DisplayLives(int lives)
        {
            return $"Remaining Lives: {lives}";
        }
        //convert values to string
        public ListBoxItem CreateListBoxItem(int NN, int ND, char[] playerArr, int length)
        {
            ListBoxItem lbi = new ListBoxItem();
            lbi.Name = "Trys";
            if(length == 4)
                lbi.Content = $"Your Code: {playerArr[0].ToString()}, {playerArr[1].ToString()}, {playerArr[2].ToString()}, {playerArr[3].ToString()}; ND: {NN}; NN: {ND}";
            if(length == 5)
                lbi.Content = $"Your Code: {playerArr[0].ToString()}, {playerArr[1].ToString()}, {playerArr[2].ToString()}, {playerArr[3].ToString()}, {playerArr[4].ToString()}; ND: {NN}; NN: {ND}";
            if(length == 6)
                lbi.Content = $"Your Code: {playerArr[0].ToString()}, {playerArr[1].ToString()}, {playerArr[2].ToString()}, {playerArr[3].ToString()}, {playerArr[4].ToString()}, {playerArr[5].ToString()}; ND: {NN}; NN: {ND}";
            return lbi;
        }
        //Generate exception comms
        public static void GenerateEx(string text)
        {
            MessageBoxResult mbr = MessageBox.Show(text);
        }

    }
}
