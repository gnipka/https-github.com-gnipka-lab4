using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Input_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string pass = passBox.Password.Trim();

            if (login != "admin")
            {
                textBoxLogin.ToolTip = "Это поле введено не корректно!";
                textBoxLogin.BorderBrush = Brushes.Red;
            }
            else if (pass != "password")
            {
                passBox.ToolTip = "Это поле введено не корректно!";
                passBox.BorderBrush = Brushes.Red;
            }
            else
            {
                textBoxLogin.ToolTip = "";
                textBoxLogin.BorderBrush = Brushes.Gray;
                passBox.ToolTip = "";
                passBox.BorderBrush = Brushes.Gray;

                MessageBox.Show("Успешно!");
                InputData inputData = new InputData();
                inputData.Show();
                Hide();
            }
        }
    }
}
