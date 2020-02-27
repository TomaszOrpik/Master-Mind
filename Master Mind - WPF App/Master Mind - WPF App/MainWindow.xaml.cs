using System;
using System.Windows;

namespace Master_Mind___WPF_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AppControl _control;
        
        public MainWindow()
        {
            InitializeComponent();
            AppView.ModeName();
        }

        private void Mode1btn_Click(object sender, RoutedEventArgs e)
        {
            _control = new AppControl();
            _control.PlayerGuess(Convert.ToInt32(ColorsBox.Text), Convert.ToInt32(LengBox.Text));
        }

        private void Mode2btn_Click(object sender, RoutedEventArgs e)
        {
            _control = new AppControl();
            _control.ComputerGuess(Convert.ToInt32(ColorsBox.Text), Convert.ToInt32(LengBox.Text), false);
        }

        private void Mode3btn_Click(object sender, RoutedEventArgs e)
        {
            _control = new AppControl();
            _control.ComputerGuess(Convert.ToInt32(ColorsBox.Text), Convert.ToInt32(LengBox.Text), true);
        }

        private void Mode4btn_Click(object sender, RoutedEventArgs e)
        {
            _control = new AppControl();
            _control.NumberGuess();
        }

        private void InputCheck()
        {
            //check for correct colors and tries input
            if (Convert.ToInt32(LengBox.SelectedItem.ToString()) >= Convert.ToInt32(ColorsBox.SelectedItem.ToString()))
            {
                AppView.GenerateEx("Number of colors must smaller than length");
                return;
            }

        }
    }
}
