using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.SqlClient;
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
using System.Data.SQLite;

namespace lab4
{
    /// <summary>
    /// Логика взаимодействия для ChangeData.xaml
    /// </summary>
    public partial class ChangeData : Window
    {
        public BindingList<Train> trains;
        AppContext db;
        public List<string> id = new List<string>();
        public List<string> category = new List<string>();
        public List<string> route = new List<string>();
        public List<string> arrival_date = new List<string>();
        public List<string> arrival_time = new List<string>();
        public List<string> departure_date = new List<string>();
        public List<string> departure_time = new List<string>();
        private SQLiteConnection DB;
        public ChangeData()
        {

            InitializeComponent();
            db = new AppContext();
            var trains = db.Trains;
            foreach(Train u in trains)
            {
                id.Add(u.id_train.ToString());
                textBoxID.ItemsSource = id;
            }
            textBoxID.IsEditable = true;
            textBoxID.Text = id[0];
            foreach(Train u in trains)
            {
                category.Add(u.Category.ToString());
            }
            textBoxCategory.Text = category[0];
            foreach (Train u in trains)
            {
                route.Add(u.Route.ToString());
            }
            textBoxRoute.Text = route[0];
            foreach (Train u in trains)
            {
                arrival_date.Add(u.Arrival_date.ToString());
            }
            datePickerArrival_Date.SelectedDate = Convert.ToDateTime(arrival_date[0]);
            foreach (Train u in trains)
            {
                arrival_time.Add(u.Arrival_time.ToString());
            }
            timePickerArrival.Text = arrival_time[0];
            foreach (Train u in trains)
            {
                departure_date.Add(u.Departure_date.ToString());
            }
            datePickerDeparture_Date.SelectedDate = Convert.ToDateTime(arrival_date[0]);
            foreach (Train u in trains)
            {
                departure_time.Add(u.Departure_time.ToString());
            }
            timePickerDeparture.Text = departure_time[0];
        }

        private void Button_OpenWindow_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            Hide();
        }

        private void textBoxID_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (!id.Contains(textBoxID.SelectedItem))
            {
                textBoxID.SelectedItem = "";
            }
        }


        private void textBoxID_DropDownClosed(object sender, EventArgs e)
        {
            int value = id.FindIndex(x => x == textBoxID.SelectedItem.ToString());
            textBoxCategory.Text = category[value];
            textBoxRoute.Text = route[value];
            datePickerArrival_Date.SelectedDate = Convert.ToDateTime(arrival_date[value]);
            timePickerArrival.Text = arrival_time[value];
            datePickerDeparture_Date.SelectedDate = Convert.ToDateTime(departure_date[value]);
            timePickerDeparture.Text = departure_time[value];

            /*Train train = new Train(category[value], route[value], arrival_date[value], arrival_time[value], departure_date[value], departure_time[value]);
            db.Trains.FindAsync(train);*/
           
        }

        private void Button_Change_Click(object sender, RoutedEventArgs e)
        {
            textBoxCategory.Text = textBoxCategory.Text.Trim();
            textBoxRoute.Text = textBoxRoute.Text.Trim();
            datePickerArrival_Date.Text = datePickerArrival_Date.Text.Trim();
            timePickerArrival.Text = timePickerArrival.Text;
            datePickerDeparture_Date.Text = datePickerDeparture_Date.Text.Trim();
            timePickerDeparture.Text = timePickerDeparture.Text;
            if (textBoxCategory.Text != "" && textBoxRoute.Text != "" && datePickerArrival_Date.Text != "" && timePickerArrival.Text != null && datePickerDeparture_Date.Text != "" && timePickerDeparture.Text != null)
            {
                string sqlExpression = String.Format("UPDATE Trains SET category = '{0}', route = '{1}', arrival_date = '{2}', arrival_time = '{3}', departure_date = '{4}', departure_time = '{5}' WHERE id_train = '{6}'", textBoxCategory.Text, textBoxRoute.Text, datePickerArrival_Date.Text, timePickerArrival.Text, datePickerDeparture_Date.Text, timePickerDeparture.Text, textBoxID.SelectedItem);
                using (DB = new SQLiteConnection(@"Data source = C:\Users\admin\source\repos\lab4\lab4\bin\Debug\train_schedule.db"))
                {
                    DB.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sqlExpression, DB))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                if (textBoxCategory.Text == "")
                {
                    textBoxCategory.ToolTip = "Это поле пустое!";
                    textBoxCategory.BorderBrush = Brushes.Red;
                }
                if (textBoxRoute.Text == "")
                {
                    textBoxRoute.ToolTip = "Это поле пустое!";
                    textBoxRoute.BorderBrush = Brushes.Red;
                }
                if (datePickerArrival_Date.Text == "")
                {
                    datePickerArrival_Date.ToolTip = "Это поле пустое!";
                    datePickerArrival_Date.BorderBrush = Brushes.Red;
                }
                if (timePickerArrival.Text == null)
                {
                    timePickerArrival.ToolTip = "Это поле пустое!";
                    timePickerArrival.BorderBrush = Brushes.Red;
                }
                if (datePickerDeparture_Date.Text == "")
                {
                    datePickerDeparture_Date.ToolTip = "Это поле пустое!";
                    datePickerDeparture_Date.BorderBrush = Brushes.Red;
                }
                if (timePickerDeparture.Text == null)
                {
                    timePickerDeparture.ToolTip = "Это поле пустое!";
                    timePickerDeparture.BorderBrush = Brushes.Red;
                }
            }

        }

        private void datePickerArrival_Date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            datePickerDeparture_Date.DisplayDateStart = datePickerArrival_Date.SelectedDate;
        }
    }
}
