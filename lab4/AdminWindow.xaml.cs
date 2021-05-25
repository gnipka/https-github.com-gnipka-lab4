﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace lab4
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    
    public partial class AdminWindow : Excel.Window
    {

        public BindingList<Train> trains;
        public Train train;
        AppContext db;
        public AdminWindow()
        {
            InitializeComponent();
            db = new AppContext();
            db.Trains.Load();
            var trains = db.Trains.Local.ToBindingList();
            dataGrid.ItemsSource = trains;
            dataGrid.UpdateLayout();
            for (int i =0; i < dataGrid.Columns.Count; i++)
            {
                dataGrid.Columns[i].IsReadOnly = true;
            }
        }

        private void Button_OpenWindow_Click(object sender, RoutedEventArgs e)
        {
            InputData inputData = new InputData();
            inputData.Show();
            Hide();
        }

        private void Button_SaveChange_Click(object sender, RoutedEventArgs e)
        {
            trains = dataGrid.DataContext as BindingList<Train>;
            dataGrid.UpdateLayout();
            try
            {
                db.SaveChanges();
            }
            catch
            { 
                //дописать ошибку
            }
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
           MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo);

            switch(result)
            {
                case MessageBoxResult.Yes:
                    if (dataGrid.SelectedItems != null && dataGrid.SelectedItems.Count > 0 && !dataGrid.Items.IsEmpty)
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
                   else
                    {
                        MessageBox.Show("Убедитесь, что вы выделили запись для удаления или, что ваша таблица имеет данные");
                    }
                    dataGrid.UpdateLayout();
                    db.SaveChanges();
                    break;
                case MessageBoxResult.No:          
                    break;
            }
        }

        private void Button_DeleteAll_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            trains = dataGrid.DataContext as BindingList<Train>;
            dataGrid.UpdateLayout();
            db.SaveChanges();
        }

        private void Button_Change_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.Items.Count != 0)
            {
                ChangeData changeData = new ChangeData();
                changeData.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Перед изменением данных, заполните таблицу");
                InputData inputData = new InputData();
                inputData.Show();
                Hide();
            }
        }

        public static void Change( Train train)
        {

        }

        private void Button_Export_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Excel.Application excel = new Excel.Application();
                excel.Visible = true; 
                Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

                for (int j = 1; j < dataGrid.Columns.Count; j++) // для названий
                {
                    Range myRange = (Range)sheet1.Cells[1, j];
                    sheet1.Cells[1, j].Font.Bold = true; //для заголовка жирным шрифтом
                    sheet1.Columns[j].ColumnWidth = 15; // регулировка ширины столбца
                    myRange.Value2 = dataGrid.Columns[j].Header;
                }
                for (int i = 1; i < dataGrid.Columns.Count; i++)
                {
                    for (int j = 0; j < dataGrid.Items.Count; j++)
                    {
                        TextBlock b = dataGrid.Columns[i].GetCellContent(dataGrid.Items[j]) as TextBlock;
                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i];
                        myRange.Value2 = b.Text;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Невозможно сохранить таблицу в Excel");
            }
        }

        dynamic Excel.Window.Activate()
        {
            throw new NotImplementedException();
        }

        public dynamic ActivateNext()
        {
            throw new NotImplementedException();
        }

        public dynamic ActivatePrevious()
        {
            throw new NotImplementedException();
        }

        public bool Close(object SaveChanges, object Filename, object RouteWorkbook)
        {
            throw new NotImplementedException();
        }

        public dynamic LargeScroll(object Down, object Up, object ToRight, object ToLeft)
        {
            throw new NotImplementedException();
        }

        public Excel.Window NewWindow()
        {
            throw new NotImplementedException();
        }

        public dynamic _PrintOut(object From, object To, object Copies, object Preview, object ActivePrinter, object PrintToFile, object Collate, object PrToFileName)
        {
            throw new NotImplementedException();
        }

        public dynamic PrintPreview(object EnableChanges)
        {
            throw new NotImplementedException();
        }

        public dynamic ScrollWorkbookTabs(object Sheets, object Position)
        {
            throw new NotImplementedException();
        }

        public dynamic SmallScroll(object Down, object Up, object ToRight, object ToLeft)
        {
            throw new NotImplementedException();
        }

        public int PointsToScreenPixelsX(int Points)
        {
            throw new NotImplementedException();
        }

        public int PointsToScreenPixelsY(int Points)
        {
            throw new NotImplementedException();
        }

        public dynamic RangeFromPoint(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void ScrollIntoView(int Left, int Top, int Width, int Height, object Start)
        {
            throw new NotImplementedException();
        }

        public dynamic PrintOut(object From, object To, object Copies, object Preview, object ActivePrinter, object PrintToFile, object Collate, object PrToFileName)
        {
            throw new NotImplementedException();
        }

        public Excel.Application Application => throw new NotImplementedException();

        public XlCreator Creator => throw new NotImplementedException();

        dynamic Excel.Window.Parent => throw new NotImplementedException();

        public Range ActiveCell => throw new NotImplementedException();

        public Chart ActiveChart => throw new NotImplementedException();

        public Pane ActivePane => throw new NotImplementedException();

        public dynamic ActiveSheet => throw new NotImplementedException();

        public dynamic Caption { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DisplayFormulas { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DisplayGridlines { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DisplayHeadings { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DisplayHorizontalScrollBar { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DisplayOutline { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool _DisplayRightToLeft { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DisplayVerticalScrollBar { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DisplayWorkbookTabs { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DisplayZeros { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool EnableResize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool FreezePanes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int GridlineColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public XlColorIndex GridlineColorIndex { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Index => throw new NotImplementedException();

        public string OnWindow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Panes Panes => throw new NotImplementedException();

        public Range RangeSelection => throw new NotImplementedException();

        public int ScrollColumn { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int ScrollRow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Sheets SelectedSheets => throw new NotImplementedException();

        public dynamic Selection => throw new NotImplementedException();

        public bool Split { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int SplitColumn { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double SplitHorizontal { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int SplitRow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double SplitVertical { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double TabRatio { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public XlWindowType Type => throw new NotImplementedException();

        public double UsableHeight => throw new NotImplementedException();

        public double UsableWidth => throw new NotImplementedException();

        public bool Visible { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Range VisibleRange => throw new NotImplementedException();

        public int WindowNumber => throw new NotImplementedException();

        XlWindowState Excel.Window.WindowState { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public dynamic Zoom { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public XlWindowView View { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DisplayRightToLeft { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public SheetViews SheetViews => throw new NotImplementedException();

        public dynamic ActiveSheetView => throw new NotImplementedException();

        public bool DisplayRuler { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool AutoFilterDateGrouping { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DisplayWhitespace { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Hwnd => throw new NotImplementedException();
    }
}
