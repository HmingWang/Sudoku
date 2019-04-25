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

            //this.GMain.Children.Clear();  
            
            Grid g = Viewer.MakeMap();
            this.GMain.Children.Add(g);
            Grid.SetColumn(g, 0);
            Grid.SetRow(g, 0);

            Viewer.WirteNumber(9, 3, 3);
            Viewer.WirteNote(4, 1, 1);
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            SudokuMap.CurrentNumber = int.Parse((sender as Button).Content.ToString());

            Button b = sender as Button;
            if (b.Tag == null || (bool)(b.Tag) == false)
            {
                b.Tag = true;
                b.Background = Brushes.Gray;
            }
            else
            {
                b.Tag = false;
                b.Background = Brushes.Silver;
            }
            //MessageBox.Show((sender as Button).Content.ToString());
        }
    }
}
