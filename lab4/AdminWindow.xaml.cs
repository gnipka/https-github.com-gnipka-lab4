using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    
    public partial class AdminWindow : Window
    {

        public BindingList<Train> trains;
        AppContext db;
        public AdminWindow()
        {
            InitializeComponent();
            db = new AppContext();
            db.Trains.Load();
            var trains = db.Trains.Local.ToBindingList();
            dataGrid.ItemsSource = trains;
            dataGrid.UpdateLayout();
            dataGrid.Columns[0].IsReadOnly = true;
        }

        private void Button_OpenWindow_Click(object sender, RoutedEventArgs e)
        {
            InputData inputData = new InputData();
            inputData.Show();
        }

        private void Button_SaveChange_Click(object sender, RoutedEventArgs e)
        {
            trains = dataGrid.DataContext as BindingList<Train>;
            dataGrid.UpdateLayout();
            db.SaveChanges();
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems != null && dataGrid.SelectedItems.Count > 0)
            {
                var toRemove = dataGrid.SelectedItems.Cast<Train>().ToList();
                //Delete logic here
                //...remove items from EF and save

                //Once confirmed remove from items source
                var items = dataGrid.ItemsSource as BindingList<Train>;
                if (items != null)
                {
                    foreach (var order in toRemove)
                    {
                        items.Remove(order);
                    }
                }
            }
            dataGrid.UpdateLayout();
            db.SaveChanges();

        }
    }
}
