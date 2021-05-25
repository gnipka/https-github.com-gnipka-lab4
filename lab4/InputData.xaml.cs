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
            if (textBoxCategory.Text != "" && textBoxRoute.Text != "" && datePickerArrival_Date.Text != "" && timePickerArrival.Text != null && datePickerDeparture_Date.Text != "" && timePickerDeparture.Text != null)
            {
                string category = textBoxCategory.Text.Trim();
                string route = textBoxRoute.Text.Trim();
                string arrival_date = datePickerArrival_Date.Text.Trim();
                string arrival_time = timePickerArrival.Text.Trim();
                string departure_date = datePickerDeparture_Date.Text.Trim();
                string departure_time = timePickerDeparture.Text.Trim();


                Train train = new Train(category, route, arrival_date, arrival_time, departure_date, departure_time);

                db.Trains.Add(train);
                db.SaveChanges();
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

        private void timePickerDeparture_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if (datePickerArrival_Date.Text == datePickerDeparture_Date.Text)
            {
                if (timePickerDeparture.Text != null && timePickerArrival.Text != null)
                {
                    List<string> timeArrival = new List<string>();
                    foreach (char ch in timePickerArrival.Text)
                    {
                        timeArrival.Add(ch.ToString());
                    }
                    List<string> timeDeparture = new List<string>();

                    foreach (char ch in timePickerDeparture.Text)
                    {
                        timeDeparture.Add(ch.ToString());
                    }
                    if (timeDeparture.Count == 8 && timeArrival.Count == 8)
                    {
                        if (timeDeparture[6] == timeArrival[6])
                        {
                            if (Convert.ToInt32(timeDeparture[1]) <= Convert.ToInt32(timeArrival[1]))
                            {
                                timeArrival[4] = (Convert.ToInt32(timeArrival[4]) + 1).ToString();
                                timePickerDeparture.Text = "";
                                foreach (string str in timeArrival)
                                {
                                    timePickerDeparture.Text += str;
                                }
                                MessageBox.Show("Введите время позднее времени отправления");
                            }

                        }
                        else if (timeDeparture[6] == "A" && timeArrival[6] == "P")
                        {

                            timeArrival[4] = (Convert.ToInt32(timeArrival[4]) + 1).ToString();
                            timePickerDeparture.Text = "";
                            foreach (string str in timeArrival)
                            {
                                timePickerDeparture.Text += str;
                            }
                            MessageBox.Show("Введите время позднее времени отправления");
                        }
                    }
                    else if (timeDeparture.Count == 7 && timeArrival.Count == 7)
                    {
                        if (timeDeparture[5] == timeArrival[5])
                        {
                            if (Convert.ToInt32(timeArrival[0]) > Convert.ToInt32(timeDeparture[0]))
                            {
                                timeArrival[3] = (Convert.ToInt32(timeArrival[3]) + 1).ToString();
                                timePickerDeparture.Text = "";
                                foreach (string str in timeArrival)
                                {
                                    timePickerDeparture.Text += str;
                                }
                                MessageBox.Show("Введите время позднее времени отправления");
                            }
                        }
                        else if (timeDeparture[5] == "A" && timeArrival[5] == "P")
                        {
                            timeArrival[3] = (Convert.ToInt32(timeArrival[3]) + 1).ToString();
                            timePickerDeparture.Text = "";
                            foreach (string str in timeArrival)
                            {
                                timePickerDeparture.Text += str;
                            }
                            MessageBox.Show("Введите время позднее времени отправления");
                        }
                    }
                }
            }
        }  
    }
}
