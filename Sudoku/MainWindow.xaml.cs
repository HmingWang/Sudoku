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
            Grid g = Viewer.MakeMainGrid();
            this.GMain.Children.Add(g);
            Grid.SetColumn(g, 0);
            Grid.SetRow(g, 0);

            Viewer.WriteNumber(9, 3, 3);
            Viewer.WirteNote(4, 1, 1);
        }

        private void btnClear()
        {
            this.btn1.Tag = null;
            this.btn2.Tag = null;
            this.btn3.Tag = null;
            this.btn4.Tag = null;
            this.btn5.Tag = null;
            this.btn6.Tag = null;
            this.btn7.Tag = null;
            this.btn8.Tag = null;
            this.btn9.Tag = null;
            this.btn1.Background = Brushes.Silver;
            this.btn2.Background = Brushes.Silver;
            this.btn3.Background = Brushes.Silver;
            this.btn4.Background = Brushes.Silver;
            this.btn5.Background = Brushes.Silver;
            this.btn6.Background = Brushes.Silver;
            this.btn7.Background = Brushes.Silver;
            this.btn8.Background = Brushes.Silver;
            this.btn9.Background = Brushes.Silver;
        }
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Viewer.CurrentNumber = int.Parse((sender as Button).Content.ToString());
            btnClear();
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
        }

        private void Cbx_notes_Checked(object sender, RoutedEventArgs e)
        {
            Viewer.IsNoteMode = (sender as CheckBox).IsChecked??false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Viewer.Reset();
        }
    }
}
