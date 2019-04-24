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

namespace Sudoku
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Label lbl = new Label();
            lbl.Content = "1";

            Label lb2 = new Label();
            lb2.Content = "2";
            this.G12.Children.Add(lbl);
            this.G12.Children.Add(lb2);
            Grid.SetColumn(lb2, 1);

            this.GMain.Children.Clear();
            this.GMain.RowDefinitions.Clear();
            this.GMain.ColumnDefinitions.Clear();
            
            Grid g = SudokuMap.MakeMap();
            this.GMain.Children.Add(g);

            //SudokuMap.WirteNumber(9, 3, 3);
            //SudokuMap.WirteNote(4, 1, 1);
        }
    }
}
