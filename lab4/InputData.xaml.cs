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
using System.Windows.Shapes;

namespace lab4
{
    /// <summary>
    /// Логика взаимодействия для InputData.xaml
    /// </summary>

    public partial class InputData : Window
    {
        AppContext db;
        public InputData()
        {
            InitializeComponent();

            db = new AppContext();

            List<Train> trains = db.Trains.ToList();
            string str = "";
            foreach (Train train in trains)
                str += train.Category + " | " + train.Route + " | " + train.Arrival_date + " | " + train.Arrival_time + " | " + train.Departure_date + " | " + train.Departure_time;

        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            string category = textBoxCategory.Text.Trim();
            string route = textBoxRoute.Text.Trim();
            string arrival_date = datePickerArrival_Date.Text.Trim();
            string arrival_time = textBoxArrival_Time.Text.Trim();
            string departure_date = datePickerDeparture_Date.Text.Trim();
            string departure_time = textBoxDeparture_Time.Text.Trim();


            Train train = new Train(category, route, arrival_date, arrival_time, departure_date, departure_time);

            db.Trains.Add(train);
            db.SaveChanges();
        }

        private void Button_OpenWindow_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            Hide();
        }

        private void datePickerArrival_Date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            datePickerDeparture_Date.DisplayDateStart = datePickerArrival_Date.SelectedDate;
        }
    }
}
