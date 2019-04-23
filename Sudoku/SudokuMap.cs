using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sudoku
{
    class SudokuMap
    {
        private static Random random = new Random();
        private static Grid GMain;
        public static Grid MakeMap()
        {
            GMain = GenerateGrid(3,3);
            Make99Grid(GMain);

            foreach (Grid i in GMain.Children)
            {
                //i.Children.Add(new Border()); 
                Make99Grid(i);
                foreach (var j in i.Children)
                {
                    if(j is Grid)
                        AddMask((Grid)j);
                }

            }

            return GMain;
        }

        private static Grid GenerateGrid(int row, int col)
        {
            Grid g = new Grid();
            while (row-->0)
                g.RowDefinitions.Add(new RowDefinition());

            while(col-->0)
                g.ColumnDefinitions.Add(new ColumnDefinition());
            
            g.Background = new SolidColorBrush(Color.FromRgb((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255)));
            g.Margin = new System.Windows.Thickness(0);
            g.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            g.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
           
            return g;
        }

        private static void Make99Grid(Grid g)
        {
            for (int i = 0; i < 9; ++i)
            {
                Grid tmp = GenerateGrid(3, 3);
                g.Children.Add(tmp);
                Grid.SetRow(tmp,i / 3 % 3);
                Grid.SetColumn(tmp, i % 3);
            }
        }

        private static void AddMask(Grid g)
        {
            for (int i = 0; i < 9; ++i)
            {
                Label tmp = new Label();
                tmp.Content = (i+1).ToString();
                g.Children.Add(tmp);
                Grid.SetRow(tmp, i / 3 % 3);
                Grid.SetColumn(tmp, i % 3);
            }
        }

        private static Grid GetGrid(int row, int col)
        {
            int R = (row - 1) / 3;
            int C = (col - 1) / 3;

            int r = (row - 1) % 3;
            int c = (col - 1) % 3;

            return (GMain.Children[R * 3 + C] as Grid).Children[r * 3 + c] as Grid;
        }

        public static void WirteNumber(int num,int row, int col)
        {
            Grid g = GetGrid(row, col);
            g.Children.Clear();
            g.RowDefinitions.Clear();
            g.ColumnDefinitions.Clear();

            Label lbl = new Label();
            lbl.Content = num.ToString();
            lbl.FontSize = 24;
            lbl.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            lbl.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            g.Children.Add(lbl);
        }

        public static void WirteNote(int num, int row, int col)
        {
            Grid g = GetGrid(row, col);
            g.Children.Clear();

            Label lbl = new Label();
            lbl.Content = num.ToString();
            g.Children.Add(lbl);

            Grid.SetRow(lbl,(num-1) / 3);
            Grid.SetColumn(lbl,(num-1) % 3);
        }
    }
}
