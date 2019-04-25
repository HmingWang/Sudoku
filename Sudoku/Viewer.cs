using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Sudoku
{
    static class Viewer
    {
        private static Grid G1; //第一级grid 顶层。
        private static Grid[,] G2; //第二级，grid 3x3
        private static Grid[,] G3;//第三级 grid 9x9
        private static int currentNumber;
        public static int CurrentNumber { get => currentNumber; set => currentNumber = value; }
        private static Random random = new Random();

        private static Grid GenerateGrid(int row, int col)
        {
            Grid g = new Grid();
            while (row-- > 0)
                g.RowDefinitions.Add(new RowDefinition());

            while (col-- > 0)
                g.ColumnDefinitions.Add(new ColumnDefinition());

            //g.Background = new SolidColorBrush(Color.FromRgb((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255)));
            //g.Margin = new Thickness(0);
            //g.HorizontalAlignment = HorizontalAlignment.Stretch;
            //g.VerticalAlignment = VerticalAlignment.Stretch;
            //g.ShowGridLines = true;
            return g;
        }
        private static void MakeG1()
        {
            G1 = GenerateGrid(3, 3);
        }
        private static void MakeG2()
        {
            G2 = new Grid[3, 3];
            int row = 0, col = 0;
            for (int i = 0; i < 9; ++i)
            {
                row = i / 3;
                col = i % 3;
                G2[row, col] = GenerateGrid(3, 3);

                G1.Children.Add(G2[row, col]);
                Grid.SetRow(G2[row, col], row);
                Grid.SetColumn(G2[row, col], col);

                //var b = new Border();
                //b.Margin = new Thickness(1);
                //b.BorderBrush = Brushes.Black;
                //b.BorderThickness = new Thickness(1);

                //Grid.SetRow(b, 0);
                //Grid.SetColumn(b, 0);
                //Grid.SetRowSpan(b, 3);
                //Grid.SetColumnSpan(b, 3);
                //G2[row, col].Children.Add(b);
            }

        }
        private static void MakeG3()
        {
            G3 = new Grid[9, 9];
            int row, col, x, y, r, c;
            for (int i = 0; i < 81; ++i)
            {
                row = i / 9 / 3;
                col = i % 9 / 3;
                r = i / 9 % 3;
                c = i % 9 % 3;
                x = i / 9;
                y = i % 9;
                G3[x, y] = GenerateGrid(3, 3);
                int[] index = new int[2];
                index[0] = x;
                index[1] = y;
                G3[x, y].Tag = index;
                G3[x, y].MouseLeftButtonDown += new MouseButtonEventHandler(MouseDown_SetNumber);
                G2[row, col].Children.Add(G3[x, y]);
                Grid.SetRow(G3[x, y], r);
                Grid.SetColumn(G3[x, y], c);

                Label lblbg = new Label();
                lblbg.HorizontalAlignment = HorizontalAlignment.Stretch;
                lblbg.VerticalAlignment = VerticalAlignment.Stretch;
                Grid.SetRow(lblbg, 0);
                Grid.SetColumn(lblbg, 0);
                Grid.SetRowSpan(lblbg, 3);
                Grid.SetColumnSpan(lblbg, 3);
                G3[x, y].Children.Add(lblbg);

                var b = new Border();
                b.Margin = new Thickness(1);
                b.BorderBrush = Brushes.Gray;
                b.BorderThickness = new Thickness(1);

                Grid.SetRow(b, 0);
                Grid.SetColumn(b, 0);
                Grid.SetRowSpan(b, 3);
                Grid.SetColumnSpan(b, 3);
                G3[x, y].Children.Add(b);
            }
        }
        private static void G3ToFront()
        {
            foreach (var i in G3)
            {
                Label lbl = new Label();
                lbl.Content = "";
                lbl.FontSize = 24;
                lbl.HorizontalAlignment = HorizontalAlignment.Center;
                lbl.VerticalAlignment = VerticalAlignment.Center;


                Grid.SetRow(lbl, 0);
                Grid.SetColumn(lbl, 0);
                Grid.SetRowSpan(lbl, 3);
                Grid.SetColumnSpan(lbl, 3);
                i.Children.Add(lbl);
            }
        }

        public static void MouseDown_SetNumber(object sender, MouseButtonEventArgs args)
        {
            if (CurrentNumber > 0)
                WirteNumber(CurrentNumber, ((sender as Grid).Tag as int[])[0], ((sender as Grid).Tag as int[])[1]);


        }
        public static Grid MakeMap()
        {
            MakeG1();
            MakeG2();
            MakeG3();

            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(240, 240, 240));
            G2[0, 0].Background = brush;
            G2[0, 2].Background = brush;
            G2[1, 1].Background = brush;
            G2[2, 0].Background = brush;
            G2[2, 2].Background = brush;

            //G3ToFront();
            return G1;
        }

        private static void AddMask(Grid g)
        {
            for (int i = 0; i < 9; ++i)
            {
                Label tmp = new Label();
                tmp.Content = (i + 1).ToString();
                tmp.MouseDoubleClick += new MouseButtonEventHandler(lbl_DoubleClick);
                g.Children.Add(tmp);
                Grid.SetRow(tmp, i / 3 % 3);
                Grid.SetColumn(tmp, i % 3);
            }
        }


        public static void WirteNumber(int num, int row, int col)
        {
            Grid g = G3[row, col];
            g.Children.Clear();
            g.RowDefinitions.Clear();
            g.ColumnDefinitions.Clear();

            Label lblbg = new Label();
            lblbg.HorizontalAlignment = HorizontalAlignment.Stretch;
            lblbg.VerticalAlignment = VerticalAlignment.Stretch;
            g.Children.Add(lblbg);

            var b = new Border();
            b.Margin = new Thickness(1);
            b.BorderBrush = Brushes.Gray;
            b.BorderThickness = new Thickness(1);

            Grid.SetRow(b, 0);
            Grid.SetColumn(b, 0);
            Grid.SetRowSpan(b, 3);
            Grid.SetColumnSpan(b, 3);
            g.Children.Add(b);


            Label lblnum = new Label();
            lblnum.Content = num.ToString(); ;
            lblnum.FontSize = 24;
            lblnum.HorizontalAlignment = HorizontalAlignment.Center;
            lblnum.VerticalAlignment = VerticalAlignment.Center;
            //lblnum.Background = Brushes.Red;
            Grid.SetRow(lblnum, 0);
            Grid.SetColumn(lblnum, 0);
            Grid.SetRowSpan(lblnum, 3);
            Grid.SetColumnSpan(lblnum, 3);
            g.Children.Add(lblnum);
        }

        public static void WirteNote(int num, int row, int col)
        {
            Grid g = G3[row, col];
            g.Children.Clear();

            Label lbl = new Label();
            lbl.Content = num.ToString();
            g.Children.Add(lbl);

            Grid.SetRow(lbl, (num - 1) / 3);
            Grid.SetColumn(lbl, (num - 1) % 3);

            var b = new Border();
            b.Margin = new Thickness(1);
            b.BorderBrush = Brushes.Gray;
            b.BorderThickness = new Thickness(1);

            Grid.SetRow(b, 0);
            Grid.SetColumn(b, 0);
            Grid.SetRowSpan(b, 3);
            Grid.SetColumnSpan(b, 3);
            g.Children.Add(b);
        }

        private static void lbl_DoubleClick(object sender, MouseButtonEventArgs arg)
        {
            MessageBox.Show((sender as Label).Content.ToString());
        }
    }
}
